namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event fired internally when an existing availability slot linked to an appointment type is updated.
/// This reflects changes in the availability for the specified clinic and appointment type.
/// </summary>
public record AppointmentTypeSlotUpdatedEvent(
    Guid ClinicId,
    Guid AppointmentTypeId,
    DateTime NewStartAt,
    DateTime NewEndAt
);
