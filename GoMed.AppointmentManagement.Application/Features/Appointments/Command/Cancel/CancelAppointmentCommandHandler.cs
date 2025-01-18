using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Enums;
using GoMed.AppointmentManagement.Domain.Events;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Cancel
{
    public class CancelAppointmentCommandHandler(
        IApplicationDbContext dbContext,
        IMediator mediator,
        IAuthUserService authUserService
    ) : IRequestHandler<CancelAppointmentCommand, Result>
    {
        public async Task<Result> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var appointment = await dbContext.Appointments.FindAsync(new object?[] { dto.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                return Result.NotFound("Appointment.NotFound", $"Appointment with Id {dto.AppointmentId} not found.");
            }

            if (!authUserService.CanAccessClinic(appointment.ClinicId))
            {
                return Result.Unauthorized("Appointment.Unauthorized", "You do not have permission to cancel this appointment.");
            }

            appointment.Status = AppointmentStatus.Cancelled;
            dbContext.Appointments.Update(appointment);
            await dbContext.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new AppointmentCancelledEvent(appointment), cancellationToken);

            return Result.Success();
        }
    }
}