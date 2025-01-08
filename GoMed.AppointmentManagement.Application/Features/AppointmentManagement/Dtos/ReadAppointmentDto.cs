namespace GoMed.AppointmentManagement.Application.Features.AppointmentManagement.Dtos;

public class ReadAppointmentDto
{
    public Guid Id { get; set; }
    public string ProfessionalId { get; set; }
    public Guid ClinicId { get; set; }
    public string PatientId { get; set; }
    public string PatientName { get; set; }
    public string PatientPhone { get; set; }
    public DateTimeOffset StartAt { get; set; }
    public DateTimeOffset EndAt { get; set; }
    public string Type { get; set; }
    public int Status { get; set; }
    public string Notes { get; set; }
    public bool ShowedUp { get; set; }
    public string BookingChannel { get; set; }
}
