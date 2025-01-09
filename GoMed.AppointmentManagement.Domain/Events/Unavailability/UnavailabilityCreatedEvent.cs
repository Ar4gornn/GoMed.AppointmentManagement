namespace GoMed.AppointmentManagement.Domain.Events.Unavailability
{
    public class UnavailabilityCreatedEvent
    {
        public GoMed.AppointmentManagement.Domain.Entities.Unavailability Unavailability { get; }

        public UnavailabilityCreatedEvent(GoMed.AppointmentManagement.Domain.Entities.Unavailability unavailability)
        {
            Unavailability = unavailability ?? throw new ArgumentNullException(nameof(unavailability));
        }
    }
}