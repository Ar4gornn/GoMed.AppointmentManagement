namespace GoMed.AppointmentManagement.Domain.Entities;

public class AppointmentType
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int DurationInMinutes { get; private set; }
    public bool IsActive { get; private set; }
    
}