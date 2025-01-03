namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class Clinic
    {
        public Guid Id { get; set; }  // Primary key
        
        public int ProfessionalId { get; set; }  // Link to a professional (foreign key)

        public string Name { get; set; }  // Clinic name
        
        public string Title { get; set; }  // Title or designation
        
        public string PictureUrl { get; set; }  // URL for clinic image/logo

        public string Speciality { get; set; }  // Clinic's specialization (e.g., Dentistry, Cardiology)

        public string Address { get; set; }  // General address

        public string DetailedAddress { get; set; }  // Additional address details (floor, building)

        public string MapUrl { get; set; }  // URL to map location
        
        public bool AllowNewPatientBooking { get; set; }  // Allow new patients to book
        
        public bool AllowPatientBooking { get; set; }  // Allow existing patients to book

        public DateTime CreatedAt { get; set; }  // Timestamp for creation
        
        public DateTime UpdatedAt { get; set; }  // Timestamp for last update

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Availability> Availabilities { get; set; }
        public ICollection<Unavailability> Unavailabilities { get; set; }
    }
}