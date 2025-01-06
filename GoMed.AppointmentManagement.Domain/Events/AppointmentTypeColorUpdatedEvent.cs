using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event fired internally when the color associated with an appointment type is updated.
    /// This event is primarily for UI updates to reflect the new color representation.
    /// </summary>
    public class AppointmentTypeColorUpdatedEvent
    {
        public AppointmentType AppointmentTypeData { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}