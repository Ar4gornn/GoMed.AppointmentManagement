using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered when an appointment is cancelled.
    /// This event informs scheduling systems to free up the allocated time slot 
    /// and sends notifications to relevant parties.
    /// </summary>
    public class AppointmentCancelledEvent
    {
        public Appointment AppointmentData { get; set; }
        public DateTimeOffset CancelledAt { get; set; }
    }
}