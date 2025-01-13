using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events.Availability;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Update.UpdateAvailability
{
    public class UpdateAvailabilityCommandHandler(
        IApplicationDbContext dbContext,
        IPublishEndpoint publishEndpoint,
        IMediator mediator,
        IAuthUserService authUserService
    ) : IRequestHandler<UpdateAvailabilityCommand, Result>
    {
        public async Task<Result> Handle(UpdateAvailabilityCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the availability by its unique Id.
            var existing = await dbContext.Availabilities
                .Include(a => a.Clinic)
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (existing == null)
            {
                return Result.NotFound("Availability.NotFound", "Availability not found.");
            }

            
            // Update the properties
            existing.DayOfWeek = request.DayOfWeek;
            existing.StartTime = request.StartTime;
            existing.EndTime = request.EndTime;

            await dbContext.SaveChangesAsync(cancellationToken);

            // Publish domain events as needed
            var @event = new AvailabilityUpdatedEvent(existing);
            await publishEndpoint.Publish(@event, cancellationToken);
            await mediator.Publish(@event, cancellationToken);

            return Result.Success();
        }
    }
}
