namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Dtos;

public class ReadAvailabilityDto
{
    public Guid ClinicId { get; set; }
    public int DayOfWeek { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}
