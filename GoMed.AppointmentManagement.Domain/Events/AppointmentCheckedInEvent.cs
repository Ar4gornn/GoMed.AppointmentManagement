using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered when a patient checks in for their appointment.
    /// This event can be used to mark the appointment as in-progress, 
    /// log the check-in time, and notify the professional or staff.
    /// </summary>
    public class AppointmentCheckInEvent
    {
        public Appointment AppointmentData { get; set; }
        public DateTimeOffset CheckedInAt { get; set; }
    }
}