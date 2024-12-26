using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Enums;

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

            builder.Property(a => a.StartAt)
                .IsRequired();

            builder.Property(a => a.EndAt)
                .IsRequired();

            builder.Property(a => a.Type)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Status)
                .IsRequired();

            builder.Property(a => a.AppointmentStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), v))
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