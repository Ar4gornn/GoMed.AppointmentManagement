using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    /// <summary>
    /// Event triggered when an appointment concludes and follow-up actions are required.
    /// This event can be used to schedule another appointment, send a survey, or handle post-visit tasks.
    /// </summary>
    public class AppointmentFollowupRequiredEvent
    {
        public Appointment AppointmentData { get; set; }
        public DateTimeOffset FollowupNeededAt { get; set; }
    }
}