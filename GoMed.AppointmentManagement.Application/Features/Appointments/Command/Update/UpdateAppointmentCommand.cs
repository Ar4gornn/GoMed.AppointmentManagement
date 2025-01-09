using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Update
{
    /// <summary>
    /// Command to update an existing appointment.
    /// </summary>
    public record UpdateAppointmentCommand(UpdateAppointmentDto Dto) : IRequest<ReadAppointmentDto>;
}