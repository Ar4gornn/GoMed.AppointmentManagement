using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Cancel
{
    /// <summary>
    /// Command to cancel an appointment.
    /// </summary>
    public record CancelAppointmentCommand(CancelAppointmentDto Dto) : IRequest;
}