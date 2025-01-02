using MediatR;
using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events
{
    public class ClinicCreatedEvent : INotification
    {
        public Clinic Clinic { get; }

        public ClinicCreatedEvent(Clinic clinic)
        {
            Clinic = clinic;
        }
    }
}