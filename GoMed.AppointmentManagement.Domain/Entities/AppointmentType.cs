namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class AppointmentType
    {
        public int Id { get; set; }  
        
        public Guid? ClinicId { get; set; }  
        
        public string? Name { get; set; }  
        
        public int DurationInMinutes { get; set; }  // Duration of the appointment in minutes
        
        public string? Color { get; set; }  // Color code for UI representation
        
        public bool AllowForPatientBooking { get; set; }  // Determines if patients can book this type directly
        
    }
}