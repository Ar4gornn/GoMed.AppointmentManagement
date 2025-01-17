namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Dtos;


/// <summary>
/// Represents the data of an existing appointment Type.
/// </summary>
public class ReadAppointmentTypeDto
{
    public int Id { get; set; }  
    public Guid? ClinicId { get; set; }  
    public string? Name { get; set; }  
    public int DurationInMinutes { get; set; }
    public string? Color { get; set; }
    public bool AllowForPatientBooking { get; set; }
}

