using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Enums;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Approve
{
    public class ApproveAppointmentCommandHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService
    ) : IRequestHandler<ApproveAppointmentCommand, Result>
    {
        public async Task<Result> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await dbContext.Appointments.FindAsync(
                new object?[] { request.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                return Result.Unauthorized("Appointment.NotFound", $"Appointment with Id {request.AppointmentId} not found.");
            }

            if (!authUserService.CanAccessClinic(appointment.ClinicId))
            {
                return Result.Unauthorized("Appointment.Unauthorized", "You do not have permission to approve this appointment.");
            }

            appointment.Status = request.IsApproved ? AppointmentStatus.Confirmed : AppointmentStatus.Cancelled;

            dbContext.Appointments.Update(appointment);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}