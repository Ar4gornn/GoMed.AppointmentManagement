using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered internally when the booking status of an appointment type is changed.
    /// This event reflects updates to whether patients can book the appointment type directly.
    /// </summary>
    public class AppointmentTypeBookingStatusChangedEvent
    {
        public AppointmentType AppointmentTypeData { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}