using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Reschedule
{
    public record RescheduleAppointmentCommand(
        Guid AppointmentId,
        DateTimeOffset StartAt,
        DateTimeOffset EndAt
    ) : IRequest<Result>;
}