using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Delete.DeleteAppointmentCommand
{
    /// <summary>
    /// Command to delete an existing appointment.
    /// </summary>
    public record DeleteAppointmentCommand(Guid AppointmentId) : IRequest;
}