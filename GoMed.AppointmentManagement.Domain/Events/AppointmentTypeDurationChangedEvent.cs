namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event triggered internally when the duration of an appointment type is changed.
/// This event ensures that all future bookings and availability slots reflect the updated duration.
/// </summary>
public record AppointmentTypeDurationChangedEvent(
    Guid AppointmentTypeId,
    int NewDurationInMinutes
);
