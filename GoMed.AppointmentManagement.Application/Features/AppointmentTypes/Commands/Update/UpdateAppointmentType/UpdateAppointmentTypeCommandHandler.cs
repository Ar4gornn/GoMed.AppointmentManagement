using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events;
using GoMed.AppointmentManagement.Domain.Events.AppointmentType;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Update.UpdateAppointmentType;

public class UpdateAppointmentTypeCommandHandler(
    IApplicationDbContext dbContext,
    IPublishEndpoint publishEndpoint,
    IMediator mediator) 
    : IRequestHandler<UpdateAppointmentType, Result<int>>
{
    public async Task<Result<int>> Handle(Update.UpdateAppointmentType.UpdateAppointmentType request, CancellationToken cancellationToken)
    {
        var appointmentType = await dbContext.AppointmentTypes
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (appointmentType is null)
        {
            return Result<int>.NotFound("AppointmentType.NotFound", "Appointment type does not exist.");
        }

        // Optional: check if new Name conflicts with an existing type in same clinic
        bool nameExists = await dbContext.AppointmentTypes
            .AnyAsync(a =>
                    a.Id != request.Id &&
                    a.ClinicId == request.ClinicId &&
                    a.Name == request.Name,
                cancellationToken);

        if (nameExists)
        {
            return Result<int>.Conflict("AppointmentType.NameConflict",
                "Another appointment type with this name already exists in the clinic.");
        }

        // Update fields
        appointmentType.ClinicId = request.ClinicId;
        appointmentType.Name = request.Name;
        appointmentType.DurationInMinutes = request.DurationInMinutes;
        appointmentType.Color = request.Color;
        appointmentType.AllowForPatientBooking = request.AllowForPatientBooking;

        dbContext.AppointmentTypes.Update(appointmentType);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Publish domain event
        var updatedEvent = new AppointmentTypeUpdatedEvent(appointmentType);
        await publishEndpoint.Publish(updatedEvent, cancellationToken);
        await mediator.Publish(updatedEvent, cancellationToken);

        return Result<int>.Success(appointmentType.Id);
    }
}
