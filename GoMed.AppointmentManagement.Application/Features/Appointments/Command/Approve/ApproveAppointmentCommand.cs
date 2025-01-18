using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Approve
{
    public record ApproveAppointmentCommand(Guid AppointmentId, bool IsApproved) 
        : IRequest<Result>;
}