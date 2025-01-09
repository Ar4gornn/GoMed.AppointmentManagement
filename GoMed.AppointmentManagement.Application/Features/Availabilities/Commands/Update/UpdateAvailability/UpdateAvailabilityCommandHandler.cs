using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
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
    public async Task<Result> Handle(UpdateAvailability request, CancellationToken cancellationToken)
    {
        // We identify an existing record by matching clinic, day of week, and (existing) start time.
        var existing = await dbContext.Availabilities
            .Include(a => a.Clinic)
            .FirstOrDefaultAsync(a =>
                    a.Clinic != null &&
                    a.Clinic.Id == request.ClinicId &&
                    a.DayOfWeek == request.DayOfWeek &&
                    a.StartTime == request.StartTime,
                cancellationToken);

        if (existing is null)
            return Result.NotFound("Availability.NotFound", "Availability not found.");

        // Update the end time
        existing.EndTime = request.EndTime;

        await dbContext.SaveChangesAsync(cancellationToken);

        // Publish domain events if needed
        var @event = new AvailabilityUpdatedEvent(existing);
        await publishEndpoint.Publish(@event, cancellationToken);
        await mediator.Publish(@event, cancellationToken);

        return Result.Success();
    }
}