using Microsoft.EntityFrameworkCore;
using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Contracts.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<AppointmentType> AppointmentTypes { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}