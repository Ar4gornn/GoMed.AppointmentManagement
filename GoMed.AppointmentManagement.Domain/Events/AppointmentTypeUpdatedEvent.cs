namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event fired internally when an existing appointment type is updated.
/// Reflects changes to the appointment type, such as name, duration, or booking availability.
/// </summary>
public record AppointmentTypeUpdatedEvent(
    Guid AppointmentTypeId,
    string Name,
    int DurationInMinutes,
    string Color,
    bool AllowForPatientBooking
);
