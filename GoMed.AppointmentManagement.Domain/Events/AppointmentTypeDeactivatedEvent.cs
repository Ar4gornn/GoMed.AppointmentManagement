using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered internally when an appointment type is deactivated, preventing further bookings.
    /// This ensures that the appointment type is no longer available for scheduling while preserving its data.
    /// </summary>
    public class AppointmentTypeDeactivatedEvent
    {
        public AppointmentType AppointmentTypeData { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}