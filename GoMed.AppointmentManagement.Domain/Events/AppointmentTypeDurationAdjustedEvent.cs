using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event fired internally when the duration of an appointment type is adjusted.
    /// This ensures that future bookings and availability reflect the new duration.
    /// </summary>
    public class AppointmentTypeDurationAdjustedEvent
    {
        public AppointmentType AppointmentTypeData { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}