using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Events;
using GoMed.AppointmentManagement.Domain.Events.Availability;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Create.CreateAvailability;

public class CreateAvailabilityCommandHandler(
    IApplicationDbContext dbContext,
    IPublishEndpoint publishEndpoint,
    IMediator mediator) : IRequestHandler<CreateAvailability, Result<int>>
{
    public async Task<Result<int>> Handle(Create.CreateAvailability.CreateAvailability request, CancellationToken cancellationToken)
    {
        // Use DateTimeOffset for comparison
        var requestStartTime = request.StartTime;
        var requestEndTime = request.EndTime;

        // Check for overlapping availability
        var overlaps = await dbContext.Availabilities.AnyAsync(a =>
                a.ClinicId == request.ClinicId &&
                a.DayOfWeek == request.DayOfWeek &&
                !(DateTimeOffset.MinValue.Add(a.EndTime.TimeOfDay) <= requestStartTime || 
                  DateTimeOffset.MinValue.Add(a.StartTime.TimeOfDay) >= requestEndTime),  // Adjusted comparison
            cancellationToken);

        if (overlaps)
        {
            return Result<int>.Conflict("Availability.OverlapExists",
                "An overlapping availability already exists for this clinic and day.");
        }

        // Store availability using DateTimeOffset
        var availability = new Availability
        {
            ClinicId = request.ClinicId,
            DayOfWeek = request.DayOfWeek,
            StartTime = request.StartTime,
            EndTime   = request.EndTime
        };

        var entry = await dbContext.Availabilities.AddAsync(availability, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var @event = new AvailabilityCreatedEvent(entry.Entity);
        await publishEndpoint.Publish(@event, cancellationToken);
        await mediator.Publish(@event, cancellationToken);

        return Result<int>.Success(entry.Entity.Id);
    }



}
