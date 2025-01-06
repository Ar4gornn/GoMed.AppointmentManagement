using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event indicating that an appointment type has been reassigned to a different clinic.
    /// </summary>
    public class AppointmentTypeClinicReassignedEvent
    {
        public AppointmentType AppointmentTypeData { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}