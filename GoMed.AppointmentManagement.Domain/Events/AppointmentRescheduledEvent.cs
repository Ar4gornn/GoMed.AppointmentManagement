using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered when an appointment is rescheduled.
    /// This event updates the schedule and notifies participants of the new time.
    /// </summary>
    public class AppointmentRescheduledEvent
    {
        public Appointment AppointmentData { get; set; }
        public DateTimeOffset RescheduledAt { get; set; }
    }
}