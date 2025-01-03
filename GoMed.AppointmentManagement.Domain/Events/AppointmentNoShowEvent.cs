namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event fired internally when a patient fails to attend their appointment without canceling.
/// This updates the appointment status and logs the no-show.
/// </summary>
public record AppointmentNoShowEvent(
    Guid AppointmentId,
    Guid PatientId,
    DateTime ScheduledTime,
    bool ShowedUp
);
