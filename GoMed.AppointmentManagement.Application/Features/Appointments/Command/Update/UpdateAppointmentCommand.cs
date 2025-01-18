using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Update
{
    public record UpdateAppointmentCommand(UpdateAppointmentDto Dto)
        : IRequest<Result<ReadAppointmentDto>>;
}