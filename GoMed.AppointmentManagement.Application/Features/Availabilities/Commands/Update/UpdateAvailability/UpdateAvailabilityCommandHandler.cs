using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events;
using GoMed.AppointmentManagement.Domain.Events.Availability;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Update.UpdateAvailability;

public class UpdateAvailabilityCommandHandler(
    IApplicationDbContext dbContext,
    IPublishEndpoint publishEndpoint,
    IMediator mediator) : IRequestHandler<UpdateAvailability, Result>
{
    public async Task<Result> Handle(Update.UpdateAvailability.UpdateAvailability request, CancellationToken cancellationToken)
    {
        var existing = await dbContext.Availabilities
            .FirstOrDefaultAsync(a => a.Id == request.AvailabilityId, cancellationToken);

        if (existing is null)
            return Result.NotFound("Availability.NotFound", "Availability not found.");

        // Optional check for overlap again, if required

        existing.DayOfWeek = request.DayOfWeek;
        existing.StartTime = DateTimeOffset.MinValue.Add(request.StartTime);
        existing.EndTime   = DateTimeOffset.MinValue.Add(request.EndTime);

        await dbContext.SaveChangesAsync(cancellationToken);

        var @event = new AvailabilityUpdatedEvent(existing);
        await publishEndpoint.Publish(@event, cancellationToken);
        await mediator.Publish(@event, cancellationToken);

        return Result.Success();
    }
}