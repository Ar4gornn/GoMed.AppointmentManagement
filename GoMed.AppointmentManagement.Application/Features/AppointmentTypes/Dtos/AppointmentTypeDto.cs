namespace GoMed.AppointmentManagement.Application.Features.AppointmentManagements.Dtos;


/// <summary>
/// Represents the data of an existing appointment Type.
/// </summary>
public class AppointmentTypeDto
{
    public int Id { get; set; }  
    public Guid? ClinicId { get; set; }  
    public string? Name { get; set; }  
    public int DurationInMinutes { get; set; }
    public string? Color { get; set; }
    public bool AllowForPatientBooking { get; set; }
}
