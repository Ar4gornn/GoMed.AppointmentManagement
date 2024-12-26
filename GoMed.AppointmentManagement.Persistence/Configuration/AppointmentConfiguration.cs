using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Persistence.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.PatientId)
                .IsRequired();

            builder.Property(a => a.ClinicId)
                .IsRequired();

            builder.Property(a => a.Date)
                .IsRequired();

            builder.Property(a => a.Status)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.CreatedBy)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.LastModifiedBy)
                .HasMaxLength(100);

            builder.Property(a => a.Created)
                .IsRequired();

            builder.Property(a => a.LastModified);

            builder.HasOne(a => a.Clinic)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClinicId);

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);
        }
    }
}