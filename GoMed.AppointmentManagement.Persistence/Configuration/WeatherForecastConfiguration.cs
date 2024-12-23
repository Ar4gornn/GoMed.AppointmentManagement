using GoMed.AppointmentManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoMed.AppointmentManagement.Persistence.Configuration;

public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Status).IsRequired();

        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasIndex(e => new { e.Name, e.Status }).IsUnique();
    }
}