using GoMed.AppointmentManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoMed.AppointmentManagement.Persistence.Configuration;

public class AvailabilityConfiguration : IEntityTypeConfiguration<Availability>
{
    public void Configure(EntityTypeBuilder<Availability> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityAlwaysColumn().HasIdentityOptions(startValue:123);

        builder.Property(e => e.ClinicId).IsRequired();
        builder.Property(e => e.DayOfWeek).IsRequired();
        builder.Property(e => e.StartTime).HasColumnType("time without timezone").IsRequired();
        builder.Property(e => e.EndTime).HasColumnType("time without timezone").IsRequired();

        builder.HasIndex(e => e.ClinicId);
    }
}