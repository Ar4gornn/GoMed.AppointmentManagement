namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event fired internally when a previously deactivated appointment type is reactivated,
/// allowing it to be booked by patients once again.
/// </summary>
public record AppointmentTypeReactivatedEvent(
    Guid AppointmentTypeId
);
