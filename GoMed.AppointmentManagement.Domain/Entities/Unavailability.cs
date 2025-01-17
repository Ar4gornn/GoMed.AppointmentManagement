namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class Unavailability
    {
        public int Id { get; set; }
        public Guid? ClinicId { get; set; }
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset EndAt { get; set; }
        public bool IsAllDay { get; set; }

        public Clinic? Clinic { get; set; }
    }
}

