using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand
{
    public record CreateAppointmentCommand(CreateAppointmentDto Dto) 
        : IRequest<Result<ReadAppointmentDto>>;
}