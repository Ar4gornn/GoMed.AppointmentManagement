using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Update
{
    public class UpdateUnavailabilityCommandHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService)
        : IRequestHandler<UpdateUnavailability, Result>
    {
        public async Task<Result> Handle(UpdateUnavailability request, CancellationToken cancellationToken)
        {
            // Check clinic access
            if (!authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result.Unauthorized("Unavailability.Unauthorized",
                    "You do not have permission to update unavailability for this clinic.");
            }

            // Find existing unavailability
            var unavailability = await dbContext.Unavailabilities
                .FindAsync(
                    request.Id ,
                    cancellationToken);

            if (unavailability is null || unavailability.ClinicId != request.ClinicId)
            {
                return Result.NotFound("Unavailability.NotFound", "Unavailability not found.");
            }

            // Update fields
            unavailability.StartAt = request.StartAt;
            unavailability.EndAt = request.EndAt;
            unavailability.IsAllDay = request.IsAllDay;

            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
