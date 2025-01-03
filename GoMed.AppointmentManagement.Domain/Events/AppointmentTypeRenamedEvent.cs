namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event triggered internally when the name of an appointment type is changed.
/// This ensures all relevant systems and records reflect the updated name.
/// </summary>
public record AppointmentTypeRenamedEvent(
    Guid AppointmentTypeId,
    string NewName
);
