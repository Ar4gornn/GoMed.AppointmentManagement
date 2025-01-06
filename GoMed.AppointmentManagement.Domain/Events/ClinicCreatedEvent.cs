using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Enums;

namespace GoMed.AppointmentManagement.Domain.Events
{
    // NOTE: In your snippet, there's 'using Clinic = GoMed.AppointmentManagement.Domain.Enums.Clinic;'
    // which suggests that 'Clinic' might be an enum, not an entity.
    // This pattern (having "ClinicData" as an object) makes the most sense if 'Clinic' is a class entity.
    // If it's truly an enum, consider renaming or changing how you store "data."
    public class ClinicCreatedEvent
    {

        public Clinic ClinicData { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}