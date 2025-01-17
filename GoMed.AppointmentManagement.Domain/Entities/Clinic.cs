namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class Clinic
    {
        public Guid Id { get; set; }  // Primary key

        public Guid ProfessionalId { get; set; }  // Link to a professional (foreign key)
        
        public string ProfessionalName { get; set; }  // Professional's name

        public string Name { get; set; }  // Clinic name

        public string Title { get; set; }  // Title or designation

        public string PictureUrl { get; set; }  // URL for clinic image/logo

        public string Speciality { get; set; }  // Clinic's specialization (e.g., Dentistry, Cardiology)

        public string Address { get; set; }  // General address

        public string DetailedAddress { get; set; }  // Additional address details (floor, building)

        public string MapUrl { get; set; }  // URL to map location

        public bool AllowNewPatientBooking { get; set; }  // Allow new patients to book

        public bool AllowPatientBooking { get; set; }  // Allow existing patients to book

        public DateTimeOffset CreatedAt { get; set; }  // Timestamp for creation

        public DateTimeOffset UpdatedAt { get; set; }  // Timestamp for last update
        
        public bool IsActive { get; set; }   // IsActive field for logical deletion

        public int PatientBookingIntervalInMinutes { get; set; } = 15;  // Interval for patient booking (in minutes)

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<Availability> Availabilities { get; set; }
        public List<Unavailability> Unavailabilities { get; set; }
    }
}