namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event triggered internally when the booking status of an appointment type is changed.
/// This event reflects updates to whether patients can book the appointment type directly.
/// </summary>
public record AppointmentTypeBookingStatusChangedEvent(
    Guid AppointmentTypeId,
    bool AllowForPatientBooking
);
