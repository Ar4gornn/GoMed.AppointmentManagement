using AppointmentManagement.Domain.Entities;
using AppointmentManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Persistence
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Availability> Availabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Appointment Configuration
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.PatientName).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Reason).HasMaxLength(250);
                entity.Property(a => a.ClinicId).IsRequired();
            });

            // Clinic Configuration
            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(150);
                entity.Property(c => c.Address).HasMaxLength(300);
                entity.Property(c => c.PhoneNumber).HasMaxLength(20);
            });

            // Patient Configuration
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Email).HasMaxLength(150);
            });

            // Availability Configuration
            modelBuilder.Entity<Availability>(entity =>
            {
                entity.HasKey(av => av.Id);
                entity.Property(av => av.ClinicId).IsRequired();
                entity.Property(av => av.AvailableDate).IsRequired();
            });

            // Concurrency Tokens
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("RowVersion")
                        .IsRowVersion();
                }
            }
        }
    }
}
