namespace GoMed.AppointmentManagement.Domain.Events.Unavailability
{
    public class UnavailabilityDeletedEvent
    {
        public int Id { get; }
        public Guid ClinicId { get; }

        public UnavailabilityDeletedEvent(int id, Guid clinicId)
        {
            Id = id;
            ClinicId = clinicId;
        }
    }
}