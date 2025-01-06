using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event fired internally when a previously deactivated appointment type is reactivated,
    /// allowing it to be booked by patients once again.
    /// </summary>
    public class AppointmentTypeReactivatedEvent
    {
        public AppointmentType AppointmentTypeData { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}