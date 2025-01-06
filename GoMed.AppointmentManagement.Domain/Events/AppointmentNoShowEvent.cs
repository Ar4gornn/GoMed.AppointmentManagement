using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered when a patient does not show up for their appointment.
    /// This event can be used to notify staff, update records, and potentially 
    /// impose cancellation or no-show policies.
    /// </summary>
    public class AppointmentNoShowEvent
    {
        public Appointment AppointmentData { get; set; }
        public DateTimeOffset NoShowAt { get; set; }
    }
}