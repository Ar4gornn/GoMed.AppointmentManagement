using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events.External;

/// <summary>
/// External Events are events that are sent from other microservices
/// </summary>
/// <param name="ExternalWeatherAnnouncementData"></param>
public record WeatherAnnouncementCreatedEvent(ExternalWeatherAnnouncementData);