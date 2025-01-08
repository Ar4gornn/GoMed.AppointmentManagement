namespace GoMed.AppointmentManagement.Application.Features.AppointmentManagement.Dtos;

public class CreateAvailabilityDto
{
    public Guid ClinicId { get; set; }
    public int DayOfWeek { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}