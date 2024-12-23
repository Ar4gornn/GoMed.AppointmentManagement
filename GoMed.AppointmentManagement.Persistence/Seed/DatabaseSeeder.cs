using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Enums;
using GoMed.AppointmentManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static class DatabaseSeeder
{
    public static async Task SeedDatabaseAsync(IHost app)
    {
        // Use the minimal API's built-in scope management
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            // Use DbContext from scoped services
            var context = services.GetRequiredService<ApplicationDbContext>();

            // Use more efficient migration approach
            await context.Database.MigrateAsync();

            // Bulk insert with efficient method
            await SeedDataIfEmptyAsync(context);
        }
        catch (Exception ex)
        {
            // Use structured logging
            var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
            logger.LogError(ex, "Database seeding failed");
        }
    }

    private static async Task SeedDataIfEmptyAsync(ApplicationDbContext context)
    {
        // Check if any data exists with more efficient method
        if (!await context.WeatherForecasts.AnyAsync())
        {
            // Use AddRange for bulk insertion
            context.WeatherForecasts.AddRange(GetInitialWeatherForecasts());

            // Save changes asynchronously
            await context.SaveChangesAsync();
        }
    }

    private static List<WeatherForecast> GetInitialWeatherForecasts() =>
    [
        new WeatherForecast
        {
            Name = "Sunny",
            Status = WeatherStatus.Normal,
            CreatedAt = DateTime.UtcNow
        },
        new WeatherForecast
        {
            Name = "Rainy",
            Status = WeatherStatus.Severe,
            CreatedAt = DateTime.UtcNow
        },
        new WeatherForecast
        {
            Name = "Cloudy",
            Status = WeatherStatus.Mild,
            CreatedAt = DateTime.UtcNow
        }
    ];

    // Extension method for easier startup configuration
    public static void SeedDatabase(this IHost app)
    {
        SeedDatabaseAsync(app).GetAwaiter().GetResult();
    }
}