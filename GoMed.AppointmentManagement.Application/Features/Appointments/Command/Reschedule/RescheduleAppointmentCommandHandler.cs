using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Reschedule
{
    public class RescheduleAppointmentCommandHandler(
        IApplicationDbContext dbContext,
        IMediator mediator,
        IAuthUserService authUserService
    ) : IRequestHandler<RescheduleAppointmentCommand, Result>
    {
        public async Task<Result> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await dbContext.Appointments.FindAsync(new object?[] { request.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                return Result.NotFound("Appointment.NotFound", $"Appointment with Id {request.AppointmentId} not found.");
            }

            if (!authUserService.CanAccessClinic(appointment.ClinicId))
            {
                return Result.Unauthorized("Appointment.Unauthorized", "You do not have permission to reschedule this appointment.");
            }

            appointment.StartAt = request.StartAt;
            appointment.EndAt = request.EndAt;

            dbContext.Appointments.Update(appointment);
            await dbContext.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new AppointmentRescheduledEvent(appointment), cancellationToken);

            return Result.Success();
        }
    }
}