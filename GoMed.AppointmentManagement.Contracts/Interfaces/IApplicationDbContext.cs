using GoMed.AppointmentManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Contracts.Interfaces;

public interface IApplicationDbContext
{
    /// <summary>
    /// This abstracts the DBContext in order to write the queries in the application layer
    /// it could be replaced by a repository pattern
    /// </summary>
    DbSet<WeatherForecast> WeatherForecasts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}