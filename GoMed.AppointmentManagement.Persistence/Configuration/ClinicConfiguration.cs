// Summary:
// This configuration class maps the Clinic entity to the Clinics table in the database.
// It defines the schema, constraints, and indexes for the Clinic entity, ensuring consistency and integrity.
//
// Key Points:
// - Primary Key: Maps Id as the unique identifier.
// - Properties: Defines required fields (Name, Address) and optional fields (PhoneNumber, Email).
// - Auditable Fields: Tracks creation and update timestamps.
// - Indexes: Ensures uniqueness for clinic names.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Enums;

namespace GoMed.AppointmentManagement.Persistence.Configurations
{
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.ToTable("Clinics");

            // Primary Key
            // Maps the Id property as the unique identifier for the Clinic table.
            builder.HasKey(c => c.Id);
            
            // Properties
            // Id: Required and manually assigned (GUID).
            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedNever(); // Explicitly set since it's a GUID

            // Name: Required, with a maximum length of 200 characters.
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Address: Required, with a maximum length of 500 characters.
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(500);

            // PhoneNumber: Optional, with a maximum length of 15 characters.
            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(15);

            // Indexes
            // Ensures that clinic names are unique in the database.
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
