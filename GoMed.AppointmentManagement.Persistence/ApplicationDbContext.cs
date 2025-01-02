using GoMed.AppointmentManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
//using GoMed.AppointmentManagement.Domain.Entities;


namespace GoMed.AppointmentManagement.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Unavailability> Unavailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(a => a.PatientName).IsRequired().HasMaxLength(100);
                entity.Property(a => a.ClinicId).IsRequired();
                entity.HasOne(a => a.Clinic)
                      .WithMany(c => c.Appointments)
                      .HasForeignKey(a => a.ClinicId);
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(150);
                entity.Property(c => c.Address).HasMaxLength(300);
            });

            modelBuilder.Entity<AppointmentType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                // Additional configurations
            });

            modelBuilder.Entity<Availability>(entity =>
            {
                entity.HasKey(av => av.Id);
                entity.HasOne(av => av.Clinic)
                      .WithMany(c => c.Availabilities)
                      .HasForeignKey(av => av.ClinicId);
            });

            modelBuilder.Entity<Unavailability>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasOne(u => u.Clinic)
                      .WithMany(c => c.Unavailabilities)
                      .HasForeignKey(u => u.ClinicId);
            });
        }
    }
}
