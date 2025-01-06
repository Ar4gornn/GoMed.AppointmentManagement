using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered when a reminder is sent for an upcoming appointment.
    /// This event helps track communication logs and notify external systems about reminders.
    /// </summary>
    public class AppointmentReminderSentEvent
    {
        public Appointment AppointmentData { get; set; }
        public DateTimeOffset ReminderSentAt { get; set; }
    }
}