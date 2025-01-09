using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand
{
    /// <summary>
    /// Command to create a new appointment.
    /// </summary>
    public record CreateAppointmentCommand(CreateAppointmentDto Dto) : IRequest<ReadAppointmentDto>;
}