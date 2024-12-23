using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events.External;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GoMed.AppointmentManagement.Application.Consumers.External;

/// <summary>
/// A Consumer behaves similarly to Mediatr Handlers, the difference is that it consumes external events comming from other microservices via MessageBus
/// This needs to be logged because it is not part of the endpoint pipeline
/// CreatedAt a new Consumer for each External Event
/// </summary>
/// <param name="logger"></param>
/// <param name="dbContext"></param>
public class ExternalWeatherAnnouncementCreatedEventConsumer(
    ILogger<ExternalWeatherAnnouncementCreatedEventConsumer> logger,

    // One can inject any Service Or repository Here
    IApplicationDbContext dbContext)
    : IConsumer<WeatherAnnouncementCreatedEvent>
{
    public async Task Consume(ConsumeContext<WeatherAnnouncementCreatedEvent> context)
    {
        logger.LogInformation(" Processing {Event} with Id {Id}", nameof(WeatherAnnouncementCreatedEvent),
            context.Message.ExternalWeatherAnnouncementData!.Id);
        try
        {
            // Add your logic here
        }
        catch (Exception exception)
        {
            // Log an error if something goes wrong
            logger.LogError(exception, "Error Processing {Event} with Id {Id}", nameof(WeatherAnnouncementCreatedEvent),
                context.Message.ExternalWeatherAnnouncementData.Id);
        }
    }
}