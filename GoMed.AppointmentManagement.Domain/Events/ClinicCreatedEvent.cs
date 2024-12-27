using MediatR;
using AppointmentManagement.Domain.Entities;

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