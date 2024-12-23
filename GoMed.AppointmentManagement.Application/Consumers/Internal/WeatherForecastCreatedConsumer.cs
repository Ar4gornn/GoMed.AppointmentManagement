using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GoMed.AppointmentManagement.Application.Consumers.Internal;

/// <summary>
/// A NotificationHandler behaves similarly to Mediatr RequestHandlers, the difference is that it consumes internal events that implement the INotification interface that are published by a mediator process
/// This needs to be logged because it is not part of the endpoint pipeline
/// Create a new Consumer for each Notification event
/// </summary>
/// <param name="logger"></param>
/// <param name="dbContext"></param>
public class WeatherForecastCreatedConsumer(
    ILogger<WeatherForecastCreatedConsumer> logger,
    IApplicationDbContext dbContext) : INotificationHandler<WeatherForecastCreatedEvent>
{
    public async Task Handle(WeatherForecastCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(" Processing {Event} with Id {Id}", nameof(WeatherForecast),
            notification.WeatherForecastData.Id);
        try
        {
            // Add your logic here
        }
        catch (Exception exception)
        {
            // Log an error if something goes wrong
            logger.LogError(exception, "Error Processing {Event} with Id {Id}", nameof(WeatherForecast),
                notification.WeatherForecastData.Id);
        }
    }
}