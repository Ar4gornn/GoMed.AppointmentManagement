using GoMed.AppointmentManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoMed.AppointmentManagement.Persistence.Configuration;

public class AppointmentTypeConfiguration: IEntityTypeConfiguration<AppointmentType>
{
    public void Configure(EntityTypeBuilder<AppointmentType> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityAlwaysColumn().HasIdentityOptions(startValue:123);
        
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.DurationInMinutes).IsRequired();
        builder.Property(e => e.Color).IsRequired().HasMaxLength(8);
        builder.Property(e => e.AllowForPatientBooking).IsRequired();

        builder.HasIndex(e => new { e.ClinicId, e.Name });
    }
}