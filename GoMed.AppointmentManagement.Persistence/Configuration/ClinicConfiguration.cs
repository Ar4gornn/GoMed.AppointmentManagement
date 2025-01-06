using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Persistence.Configuration
{
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.ToTable("Clinics");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties
            // Allow EF to auto-generate Id
            builder.Property(c => c.Id);

            // Name: Required, with a maximum length of 200 characters.
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Title: Optional, with a maximum length of 100 characters.
            builder.Property(c => c.Title)
                .HasMaxLength(100);

            // PictureUrl: Optional, with a maximum length of 300 characters.
            builder.Property(c => c.PictureUrl)
                .HasMaxLength(300);

            // Speciality: Optional, with a maximum length of 150 characters.
            builder.Property(c => c.Speciality)
                .HasMaxLength(150);

            // Address: Required, with a maximum length of 500 characters.
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(500);

            // DetailedAddress: Optional, with a maximum length of 500 characters.
            builder.Property(c => c.DetailedAddress)
                .IsRequired()
                .HasMaxLength(500);

            // MapUrl: Optional, with a maximum length of 300 characters.
            builder.Property(c => c.MapUrl)
                .HasMaxLength(300);

            // AllowNewPatientBooking: Required boolean
            builder.Property(c => c.AllowNewPatientBooking)
                .IsRequired();

            // AllowPatientBooking: Required boolean
            builder.Property(c => c.AllowPatientBooking)
                .IsRequired();

            // ProfessionalId: Required
            builder.Property(c => c.ProfessionalId)
                .IsRequired();

            // ProfessionalName: Required, with a maximum length of 200 characters.
            builder.Property(c => c.ProfessionalName)
                .IsRequired()
                .HasMaxLength(200);

            // Indexes
            // Add index for ProfessionalId to optimize queries
            builder.HasIndex(c => c.ProfessionalId);
        }
    }
}