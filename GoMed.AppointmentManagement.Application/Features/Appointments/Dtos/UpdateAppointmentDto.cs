namespace GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;

public class UpdateAppointmentDto
{
    public Guid Id { get; set; }
    public string PatientId { get; set; }
    public string PatientName { get; set; }
    public string PatientPhone { get; set; }
    public string Type { get; set; }
    public string Notes { get; set; }
    public DateTimeOffset NewStartTime { get; set; }
    public DateTimeOffset? NewEndTime { get; set; }
}
