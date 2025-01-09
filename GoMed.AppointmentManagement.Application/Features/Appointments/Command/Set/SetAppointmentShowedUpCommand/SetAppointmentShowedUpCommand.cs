using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Set.SetAppointmentShowedUpCommand
{
    /// <summary>
    /// Command to set the 'ShowedUp' flag for an appointment.
    /// </summary>
    public record SetAppointmentShowedUpCommand(Guid AppointmentId, bool ShowedUp) : IRequest;
}