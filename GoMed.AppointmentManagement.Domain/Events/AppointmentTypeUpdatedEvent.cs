using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event fired internally when an existing appointment type is updated.
    /// Reflects changes to the appointment type, such as name, duration, or booking availability.
    /// </summary>
    public class AppointmentTypeUpdatedEvent
    {
        public AppointmentType AppointmentTypeData { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}