using MediatR;
using GoMed.AppointmentManagement.Domain.Entities;
using Clinic = GoMed.AppointmentManagement.Domain.Enums.Clinic;

namespace GoMed.AppointmentManagement.Domain.Events
{
    public class ClinicCreatedEvent
    {
        public Clinic Clinic { get; }

        public ClinicCreatedEvent(Clinic clinic)
        {
            Clinic = clinic;
        }
    }
}