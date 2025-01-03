namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event triggered internally when a patient checks in for their appointment.
/// This event updates the system to mark the patient as arrived.
/// </summary>
/// <param name="AppointmentData"></param>
public record AppointmentCheckedInEvent(
    Guid AppointmentId,
    Guid PatientId,
    DateTime CheckInTime,
    bool ShowedUp
);
