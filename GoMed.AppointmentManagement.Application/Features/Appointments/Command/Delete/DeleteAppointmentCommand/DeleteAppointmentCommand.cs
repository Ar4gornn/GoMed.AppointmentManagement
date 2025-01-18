using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Delete.DeleteAppointmentCommand
{
    public record DeleteAppointmentCommand(Guid AppointmentId) 
        : IRequest<Result>;
}