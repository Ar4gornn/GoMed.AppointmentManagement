using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
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
    public async Task<Result<int>> Handle(CreateAvailability request, CancellationToken cancellationToken)
    {
        // Check if availability already exists for the clinic, day, and time
        var exists = await dbContext.Availabilities
            .AnyAsync(a =>
                a.Clinic != null &&
                a.Clinic.Id == request.ClinicId &&
                a.DayOfWeek == request.DayOfWeek &&
                a.StartTime == request.StartTime,
                cancellationToken);

        if (exists)
        {
            return Result<int>.Conflict("Availability.Conflict", "Availability already exists.");
        }

        // Find the related clinic or return an error if not found
        var clinic = await dbContext.Clinics
            .FirstOrDefaultAsync(c => c.Id == request.ClinicId, cancellationToken);

        if (clinic == null)
        {
            return Result<int>.NotFound("Clinic.NotFound", "Clinic not found.");
        }

        // Create new availability
        var availability = new Availability
        {
            Clinic = clinic,
            DayOfWeek = request.DayOfWeek,
            StartTime = request.StartTime,
            EndTime = request.EndTime
        };

        dbContext.Availabilities.Add(availability);
        await dbContext.SaveChangesAsync(cancellationToken);

        // If you need to publish an event:
        var @event = new AvailabilityCreatedEvent(availability);
        await publishEndpoint.Publish(@event, cancellationToken);
        await mediator.Publish(@event, cancellationToken);

        // Since we do not have an ID, you could return a generic "1" or "0" or switch to Result.Success() if you prefer.
        return Result<int>.Success(1);
    }
}
