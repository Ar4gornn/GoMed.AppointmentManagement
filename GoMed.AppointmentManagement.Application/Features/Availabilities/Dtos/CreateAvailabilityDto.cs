namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Dtos;

public class CreateAvailabilityDto
{
    public int? Id { get; set; }
    public Guid ClinicId { get; set; }
    public int DayOfWeek { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}