namespace GoMed.AppointmentManagement.Application.Features.AppointmentManagement.Dtos;

public class CreateUnavailabilityDto
{
    public Guid ClinicId { get; set; }
    public DateTimeOffset StartDateTime { get; set; }  // DateTime for combined date and time
    public DateTimeOffset EndDateTime { get; set; }    // DateTime for combined date and time
    public bool IsAllDay { get; set; }
}