namespace AppointmentManagement.Domain.Entities
{
    public class Unavailability
    {
        public Guid Id { get; set; }
        public Guid ClinicId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAllDay { get; set; }

        public Clinic Clinic { get; set; }
    }
}