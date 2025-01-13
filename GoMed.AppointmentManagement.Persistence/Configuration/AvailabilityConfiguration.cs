using GoMed.AppointmentManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoMed.AppointmentManagement.Persistence.Configuration
{
    public class AvailabilityConfiguration : IEntityTypeConfiguration<Availability>
    {
        public void Configure(EntityTypeBuilder<Availability> builder)
        {
            // Set the primary key
            builder.HasKey(a => a.Id);

            // Configure auto-generation of the primary key if necessary (for int type)
            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            // Configure required properties
            builder.Property(a => a.StartTime)
                .IsRequired();

            builder.Property(a => a.EndTime)
                .IsRequired();

            // Configure DayOfWeek (if needed, maybe add some constraint or comment)
            builder.Property(a => a.DayOfWeek)
                .IsRequired();

            // Configure the relationship with Clinic if needed
            builder.HasOne(a => a.Clinic)
                .WithMany(c => c.Availabilities)  // Assuming Clinic has an ICollection<Availability> Availabilities property.
                .HasForeignKey("ClinicId")        // Optionally specify the FK property name if it's shadow property or explicit.
                .OnDelete(DeleteBehavior.Cascade);

            // Optional: Configure the table name
            builder.ToTable("Availabilities");
        }
    }
}