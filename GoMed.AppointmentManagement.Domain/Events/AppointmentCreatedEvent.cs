using AppointmentManagement.Domain.Entities;
using MediatR;

namespace GoMed.AppointmentManagement.Domain.Events
{
    public class AppointmentCreatedEvent : INotification
    {
        public Appointment Appointment { get; }

        public AppointmentCreatedEvent(Appointment appointment)
        {
            Appointment = appointment;
        }
    }
}