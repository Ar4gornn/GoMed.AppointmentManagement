using Microsoft.EntityFrameworkCore;
using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Contracts.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<AppointmentType> AppointmentTypes { get; }
        DbSet<Clinic> Clinics { get; }
        DbSet<Unavailability> Unavailabilities { get; }
        DbSet<Appointment> Appointments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}