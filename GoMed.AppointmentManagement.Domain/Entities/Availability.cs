namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class Availability
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public Clinic Clinic { get; set; }
    }
}