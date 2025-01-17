using GoMed.AppointmentManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoMed.AppointmentManagement.Persistence.Configuration;

public class UnavailabilityConfiguration: IEntityTypeConfiguration<Unavailability>
{
    public void Configure(EntityTypeBuilder<Unavailability> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityAlwaysColumn().HasIdentityOptions(startValue:123);
        
        builder.Property(e => e.ClinicId).IsRequired();
        builder.Property(e => e.StartAt).IsRequired();
        builder.Property(e => e.IsAllDay).IsRequired();
        
        builder.HasIndex(e => e.ClinicId);
    }
}