namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event triggered internally when the name of an appointment type is changed.
/// This ensures that all references to the appointment type are updated consistently across the system.
/// </summary>
public record AppointmentTypeNameChangedEvent(
    Guid AppointmentTypeId,
    string NewName
);
