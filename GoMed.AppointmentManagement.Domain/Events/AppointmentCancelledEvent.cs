using GoMed.AppointmentManagement.Domain.Enums;

namespace GoMed.AppointmentManagement.Domain.Events;


/// <summary>
/// Event triggered internally when an appointment is canceled.
/// This event updates the status and ensures the slot is freed up for new bookings.
/// </summary>
/// <param name="AppointmentData"></param>
public record AppointmentCancelledEvent(
    Guid AppointmentId,
    AppointmentStatus Status,
    string CancelledBy,
    DateTime CancelledAt
);
