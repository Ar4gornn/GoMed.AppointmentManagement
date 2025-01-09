namespace GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;


/// <summary>
/// Represents the data of an existing appointment.
/// </summary>
public class ReadAppointmentDto
{
    public Guid ProfessionalId { get; init; }
    public Guid ClinicId { get; init; }
    public Guid PatientId { get; init; }
    public string? PatientName { get; init; }
    public string? PatientPhone { get; init; }
    public DateTimeOffset StartAt { get; init; }
    public DateTimeOffset EndAt { get; init; }
    public string? Type { get; init; }
    public string? Notes { get; init; }
    public bool ShowedUp { get; init; }
}