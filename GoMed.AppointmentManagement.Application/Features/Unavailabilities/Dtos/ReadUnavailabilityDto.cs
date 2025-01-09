namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Dtos;

public class ReadUnavailabilityDto
{
    public int Id { get; set; }
    public Guid ClinicId { get; set; }
    public DateTimeOffset StartDateTime { get; set; }  
    public DateTimeOffset EndDateTime { get; set; }    
    public bool IsAllDay { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}