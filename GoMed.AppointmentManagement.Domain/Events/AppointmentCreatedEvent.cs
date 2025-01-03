using GoMed.AppointmentManagement.Domain.Enums;

namespace GoMed.AppointmentManagement.Domain.Events;

/// <summary>
/// Event triggered internally when a new appointment is successfully created.
/// This event updates internal records, schedules, and initiates relevant notifications.
/// </summary>
/// <param name="AppointmentData"></param>
public record AppointmentCreatedEvent(
    Guid AppointmentId,
    Guid ProfessionalId,
    Guid ClinicId,
    Guid PatientId,
    string PatientName,
    DateTime StartAt,
    DateTime EndAt,
    string Type,
    BookingChannel BookingChannel
);
