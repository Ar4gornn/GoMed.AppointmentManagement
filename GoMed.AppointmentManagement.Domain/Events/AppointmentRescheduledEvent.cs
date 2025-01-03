namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event fired internally when an appointment is rescheduled.
/// This event updates the appointment timings and reflects the change in all related schedules.
/// </summary>
/// <param name="AppointmentData"></param>
public record AppointmentRescheduledEvent(
    Guid AppointmentId,
    DateTime NewStartAt,
    DateTime NewEndAt,
    string RescheduledBy
);
