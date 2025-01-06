using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered internally when an appointment type is deleted from a clinic's system.
    /// This event ensures that any slots or references to the appointment type are handled appropriately.
    /// </summary>
    public class AppointmentTypeDeletedEvent
    {
        public AppointmentType AppointmentTypeData { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}