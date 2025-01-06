using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered when a patient checks out after their appointment.
    /// This event finalizes the visit, notifies billing (if applicable), 
    /// and frees up the resource for subsequent appointments.
    /// </summary>
    public class AppointmentCheckOutEvent
    {
        public Appointment AppointmentData { get; set; }
        public DateTimeOffset CheckedOutAt { get; set; }
    }
}