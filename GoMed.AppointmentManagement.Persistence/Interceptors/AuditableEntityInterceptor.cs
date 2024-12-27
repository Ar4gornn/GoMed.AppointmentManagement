using GoMed.AppointmentManagement.Contracts.Interfaces;
using AppointmentManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GoMed.AppointmentManagement.Persistence.Interceptors;

/// <summary>
/// Interceptor to update the audit fields of the entities
/// When an entity is added or modified, the CreatedBy, CreatedAt, LastModifiedBy, LastModified fields are updated automatically
/// </summary>
/// <param name="user"></param>
public class AuditableEntityInterceptor(IAuthUserService user) : SaveChangesInterceptor
{
    public abstract class AuditableEntityBase
    {
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<AuditableEntityBase>())
        {
            if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = $"{user.UserType} - {user.UserId.ToString()}";
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                }

                entry.Entity.LastModifiedBy = $"{user.UserType} - {user.UserId}";
                entry.Entity.LastModified = DateTimeOffset.UtcNow;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
}