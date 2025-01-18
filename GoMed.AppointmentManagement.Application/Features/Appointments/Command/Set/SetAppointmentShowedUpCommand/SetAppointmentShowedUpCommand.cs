using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Set.SetAppointmentShowedUpCommand
{
    public record SetAppointmentShowedUpCommand(Guid AppointmentId, bool ShowedUp) 
        : IRequest<Result>;
}