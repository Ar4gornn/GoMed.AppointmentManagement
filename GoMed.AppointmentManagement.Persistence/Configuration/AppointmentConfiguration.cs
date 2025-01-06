using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Persistence.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            // Configure primary key
            builder.HasKey(a => a.Id);

            // Apply constraints to PatientName (e.g., max length 100)
            builder.Property(a => a.PatientName)
                .IsRequired()
                .HasMaxLength(100);

            // Apply constraints to PatientPhone (e.g., max length 15)
            builder.Property(a => a.PatientPhone)
                .HasMaxLength(20);

            // Apply constraints to Notes
            builder.Property(a => a.Notes)
                .HasMaxLength(500);

            // Enum property - Use native type directly
            builder.Property(a => a.Type)
                .IsRequired();

            // Enum Status is required
            builder.Property(a => a.Status)
                .IsRequired();

            // Relationship with Clinic entity
            builder.HasOne<Clinic>(a => a.Clinic)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClinicId);

            // Index ClinicId and PatientId
            builder.HasIndex(a => a.ClinicId);
            builder.HasIndex(a => a.PatientId);
            
        }
    }
}
