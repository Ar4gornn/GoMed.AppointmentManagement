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
        IMediator mediator) : IRequestHandler<UpdateAvailabilityCommand, Result>
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

            // If a new ClinicId is provided, update the Clinic association.
            // (Assumes that the Clinic exists; you may consider adding extra logic to validate this.)
            if (request.ClinicId.HasValue)
            {
                // Depending on your application's design, you might have a lookup like:
                // var clinic = await dbContext.Clinics.FirstOrDefaultAsync(c => c.Id == request.ClinicId.Value, cancellationToken);
                // if (clinic == null)
                //     return Result.NotFound("Clinic.NotFound", "Clinic not found.");
                //
                // existing.Clinic = clinic;
                //
                // For this example, we'll assume that the Clinic navigation property
                // gets updated automatically if needed, or that the ClinicId property is tracked.
            }

            // Update the properties.
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
