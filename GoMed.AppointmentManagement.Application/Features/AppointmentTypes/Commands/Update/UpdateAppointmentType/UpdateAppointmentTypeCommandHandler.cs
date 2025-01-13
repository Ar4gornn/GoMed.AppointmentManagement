using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events.AppointmentType;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Update.UpdateAppointmentType
{
    public class UpdateAppointmentTypeCommandHandler(
        IApplicationDbContext dbContext,
        IPublishEndpoint publishEndpoint,
        IMediator mediator,
        IAuthUserService authUserService
    ) : IRequestHandler<UpdateAppointmentType, Result<int>>
    {
        public async Task<Result<int>> Handle(UpdateAppointmentType request, CancellationToken cancellationToken)
        {
            var appointmentType = await dbContext.AppointmentTypes
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (appointmentType is null)
            {
                return Result<int>.NotFound("AppointmentType.NotFound", "Appointment type does not exist.");
            }

            // Check if the current ClinicId is valid and accessible
            if (!appointmentType.ClinicId.HasValue || !authUserService.CanAccessClinic(appointmentType.ClinicId.Value))
            {
                return Result<int>.Forbidden("AppointmentType.Forbidden",
                    "You do not have permission to update appointment types in this clinic.");
            }
            

            // Check if a different appointment type with the same name exists in the target clinic
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
}
