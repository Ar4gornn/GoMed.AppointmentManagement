using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered internally when the name of an appointment type is changed.
    /// This ensures all relevant systems and records reflect the updated name.
    /// </summary>
    public class AppointmentTypeRenamedEvent
    {
        public AppointmentType AppointmentTypeData { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}