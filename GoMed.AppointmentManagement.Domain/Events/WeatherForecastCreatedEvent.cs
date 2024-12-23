using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;

namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// This event will be published internally using NotificationPublisher and externally using MassTransit's PublishEndpoint
/// If it won't be published internally, you can remove the INotification interface
/// </summary>
/// <param name="WeatherForecastData"></param>
public record WeatherForecastCreatedEvent(WeatherForecast WeatherForecastData) : INotification;