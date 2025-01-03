namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event triggered internally when a new availability slot is created for a specific appointment type.
/// This reflects the addition of a new time slot based on the appointment type and clinic schedule.
/// </summary>
public record AppointmentTypeSlotCreatedEvent(
    Guid ClinicId,
    Guid AppointmentTypeId,
    DateTime StartAt,
    DateTime EndAt
);
