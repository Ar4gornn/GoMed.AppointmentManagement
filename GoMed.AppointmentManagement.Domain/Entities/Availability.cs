namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class Availability
    {
        public int Id { get; set; }
        public Guid ClinicId { get; set; }
        public int DayOfWeek { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }

        public Clinic Clinic { get; set; }
    }
}