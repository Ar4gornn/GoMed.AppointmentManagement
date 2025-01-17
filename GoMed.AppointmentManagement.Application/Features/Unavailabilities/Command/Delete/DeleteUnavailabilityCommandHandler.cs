using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events.Unavailability;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Delete.DeleteUnavailability
{
    public class DeleteUnavailabilityCommandHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService)
        : IRequestHandler<DeleteUnavailability, Result>
    {
        public async Task<Result> Handle(DeleteUnavailability request, CancellationToken cancellationToken)
        {
            // Check clinic access
            if (!authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result.Unauthorized("Unavailability.Unauthorized",
                    "You do not have permission to delete unavailability for this clinic.");
            }

            var unavailability = await dbContext.Unavailabilities
                .FindAsync(request.Id, cancellationToken);

            if (unavailability is null || unavailability.ClinicId != request.ClinicId)
            {
                return Result.NotFound("Unavailability.NotFound", "Unavailability not found.");
            }

            dbContext.Unavailabilities.Remove(unavailability);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
