namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class Unavailability
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAllDay { get; set; }

        public Clinic Clinic { get; set; }
    }
}