namespace GoMed.AppointmentManagement.Domain.Events.Unavailability
{
    public class UnavailabilityUpdatedEvent
    {
        public GoMed.AppointmentManagement.Domain.Entities.Unavailability Unavailability { get; }

        public UnavailabilityUpdatedEvent(GoMed.AppointmentManagement.Domain.Entities.Unavailability unavailability)
        {
            Unavailability = unavailability ?? throw new ArgumentNullException(nameof(unavailability));
        }
    }
}