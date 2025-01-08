using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events;
using GoMed.AppointmentManagement.Domain.Events.Availability;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Delete.DeleteAvailability;

public class DeleteAvailabilityCommandHandler(
    IApplicationDbContext dbContext,
    IPublishEndpoint publishEndpoint,
    IMediator mediator) : IRequestHandler<DeleteAvailability, Result>
{
    public async Task<Result> Handle(Delete.DeleteAvailability.DeleteAvailability request, CancellationToken cancellationToken)
    {
        var existing = await dbContext.Availabilities
            .FirstOrDefaultAsync(a => a.Id == request.AvailabilityId, cancellationToken);

        if (existing is null)
            return Result.NotFound("Availability.NotFound", "Availability not found.");

        dbContext.Availabilities.Remove(existing);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Pass the entire 'existing' entity to the event
        var @event = new AvailabilityDeletedEvent(existing);
        await publishEndpoint.Publish(@event, cancellationToken);
        await mediator.Publish(@event, cancellationToken);

        return Result.Success();
    }

}