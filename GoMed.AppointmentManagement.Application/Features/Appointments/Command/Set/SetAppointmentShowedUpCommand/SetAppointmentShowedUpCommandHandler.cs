using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Set.SetAppointmentShowedUpCommand
{
    public class SetAppointmentShowedUpCommandHandler(
        IApplicationDbContext dbContext,
        IMediator mediator,
        IAuthUserService authUserService
    ) : IRequestHandler<SetAppointmentShowedUpCommand, Result>
    {
        public async Task<Result> Handle(SetAppointmentShowedUpCommand request, CancellationToken cancellationToken)
        {
            var appointment = await dbContext.Appointments.FindAsync(new object?[] { request.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                return Result.NotFound("Appointment.NotFound", $"Appointment with Id {request.AppointmentId} not found.");
            }

            if (!authUserService.CanAccessClinic(appointment.ClinicId))
            {
                return Result.Unauthorized("Appointment.Unauthorized", "You do not have permission to update this appointment.");
            }

            appointment.ShowedUp = request.ShowedUp;

            if (request.ShowedUp)
            {
                appointment.Status = Domain.Enums.AppointmentStatus.Completed;
                await mediator.Publish(new AppointmentCheckOutEvent(appointment), cancellationToken);
            }
            else
            {
                await mediator.Publish(new AppointmentNoShowEvent(appointment), cancellationToken);
            }

            dbContext.Appointments.Update(appointment);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}