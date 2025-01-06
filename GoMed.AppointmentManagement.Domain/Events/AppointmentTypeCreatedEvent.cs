using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered internally when a new appointment type is created for a clinic.
    /// This event ensures that the system reflects the addition of a new appointment type,
    /// allowing it to be used in scheduling and bookings.
    /// </summary>
    public class AppointmentTypeCreatedEvent
    {
        public AppointmentType AppointmentTypeData { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}