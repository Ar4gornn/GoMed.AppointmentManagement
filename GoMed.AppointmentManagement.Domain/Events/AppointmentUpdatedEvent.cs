using GoMed.AppointmentManagement.Domain.Enums;

namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event fired internally when an existing appointment is updated.
/// This event ensures that all relevant records and schedules reflect the new appointment details.
/// </summary>
/// <param name="AppointmentData"></param>
public record AppointmentUpdatedEvent(
    Guid AppointmentId,
    DateTime StartAt,
    DateTime EndAt,
    AppointmentStatus Status,
    string UpdatedBy
);

