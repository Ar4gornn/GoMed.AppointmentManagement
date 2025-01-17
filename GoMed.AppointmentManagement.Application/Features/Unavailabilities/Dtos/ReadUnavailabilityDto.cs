namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Dtos;

public class ReadUnavailabilityDto
{
    public int Id { get; set; }
    public Guid ClinicId { get; set; }
    public DateTimeOffset StartAt { get; set; }  
    public DateTimeOffset EndAt { get; set; }    
    public bool IsAllDay { get; set; }
}