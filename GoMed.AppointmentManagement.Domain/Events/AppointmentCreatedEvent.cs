using GoMed.AppointmentManagement.Domain.Entities;


namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered internally when a new appointment is successfully created.
    /// This event updates internal records, schedules, and initiates relevant notifications.
    /// </summary>
    public class AppointmentCreatedEvent
    {
        public Appointment AppointmentData { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
