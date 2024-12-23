using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Application.Features.WeatherForecasts.Dtos;

public class ReadWeatherForecastDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    /// <summary>
    /// Create your own mapper
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static ReadWeatherForecastDto MapFromEntity(WeatherForecast entity)
    {
        return new ReadWeatherForecastDto()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}