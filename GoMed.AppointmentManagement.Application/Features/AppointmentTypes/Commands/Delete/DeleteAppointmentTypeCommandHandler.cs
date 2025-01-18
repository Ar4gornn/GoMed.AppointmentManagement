using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Delete
{
    public class DeleteAppointmentTypeCommandHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService
    ) : IRequestHandler<DeleteAppointmentType, Result<int>>
    {
        public async Task<Result<int>> Handle(DeleteAppointmentType request, CancellationToken cancellationToken)
        {
            var appointmentType = await dbContext.AppointmentTypes
                .FirstOrDefaultAsync(a => a.Id == request.Id && a.ClinicId == request.ClinicId, cancellationToken);

            if (appointmentType == null)
            {
                return Result<int>.NotFound("AppointmentType.NotFound", "Appointment type does not exist.");
            }

            // Check clinic access
            if (!appointmentType.ClinicId.HasValue || !authUserService.CanAccessClinic(appointmentType.ClinicId.Value))
            {
                return Result<int>.Unauthorized("AppointmentType.Unauthorized",
                    "You do not have permission to delete appointment types from this clinic.");
            }

            dbContext.AppointmentTypes.Remove(appointmentType);
            await dbContext.SaveChangesAsync(cancellationToken);

            // Publish domain event

            return Result<int>.Success(appointmentType.Id);
        }
    }
}