namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class Availability
    {
        public int DayOfWeek { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}