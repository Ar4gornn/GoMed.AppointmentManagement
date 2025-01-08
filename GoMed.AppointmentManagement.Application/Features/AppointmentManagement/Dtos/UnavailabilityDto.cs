namespace GoMed.AppointmentManagement.Application.Features.AppointmentManagement.Dtos;

public class UnavailabilityDto
{
    public Guid Id { get; set; }
    public Guid ClinicId { get; set; }
    public DateTimeOffset StartDateTime { get; set; }  // Combine Date and TimeSpan to DateTime
    public DateTimeOffset EndDateTime { get; set; }    // Combine Date and TimeSpan to DateTime
    public bool IsAllDay { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}