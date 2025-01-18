using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Cancel
{
    public record CancelAppointmentCommand(CancelAppointmentDto Dto) 
        : IRequest<Result>;
}