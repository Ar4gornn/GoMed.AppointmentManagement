using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Delete.DeleteAppointmentCommand
{
    public class DeleteAppointmentCommandHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService
    ) : IRequestHandler<DeleteAppointmentCommand, Result>
    {
        public async Task<Result> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await dbContext.Appointments.FindAsync(new object?[] { request.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                return Result.NotFound("Appointment.NotFound", $"Appointment with Id {request.AppointmentId} not found.");
            }

            if (!authUserService.CanAccessClinic(appointment.ClinicId))
            {
                return Result.Unauthorized("Appointment.Unauthorized", "You do not have permission to delete this appointment.");
            }

            dbContext.Appointments.Remove(appointment);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}