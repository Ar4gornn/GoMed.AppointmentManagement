using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Approve
{
    /// <summary>
    /// Command to approve or decline an appointment.
    /// </summary>
    public record ApproveAppointmentCommand(Guid AppointmentId, bool IsApproved) : IRequest;
}