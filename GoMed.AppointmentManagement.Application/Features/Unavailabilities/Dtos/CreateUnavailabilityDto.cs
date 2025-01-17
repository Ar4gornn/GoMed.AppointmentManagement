namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Dtos;

public class CreateUnavailabilityDto
{
    public Guid ClinicId { get; set; }
    public DateTimeOffset StartDateTime { get; set; }
    public DateTimeOffset EndDateTime { get; set; }
    public bool IsAllDay { get; set; }
}