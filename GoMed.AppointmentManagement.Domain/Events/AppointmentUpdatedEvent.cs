using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered when an existing appointment is updated.
    /// This event notifies relevant systems of any changes to the appointment details.
    /// </summary>
    public class AppointmentUpdatedEvent
    {
        public Appointment AppointmentData { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}