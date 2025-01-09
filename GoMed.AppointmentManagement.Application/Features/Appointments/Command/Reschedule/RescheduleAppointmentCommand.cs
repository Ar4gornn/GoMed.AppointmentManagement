using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Reschedule
{
    /// <summary>
    /// Command to reschedule an existing appointment.
    /// </summary>
    public record RescheduleAppointmentCommand(Guid AppointmentId, DateTimeOffset StartAt, DateTimeOffset EndAt) : IRequest;
}