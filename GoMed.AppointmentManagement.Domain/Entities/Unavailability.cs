namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class Unavailability
    {
        public int Id { get; set; }
        public Guid? ClinicId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public bool IsAllDay { get; set; }

        public Clinic? Clinic { get; set; }
    }
}

