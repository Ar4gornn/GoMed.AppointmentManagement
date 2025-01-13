using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Create.CreateAppointmentType
{
    public class CreateAppointmentTypeCommandHandler(
        IApplicationDbContext dbContext,
        IPublishEndpoint publishEndpoint,
        IMediator mediator,
        IAuthUserService authUserService
    ) : IRequestHandler<CreateAppointmentType, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateAppointmentType request, CancellationToken cancellationToken)
        {
            // Check clinic access
            if (!authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result<int>.Forbidden("AppointmentType.Forbidden",
                    "You do not have permission to create an appointment type for this clinic.");
            }

            // Ensure the name is unique within the same clinic
            bool nameExists = await dbContext.AppointmentTypes
                .AnyAsync(a => a.ClinicId == request.ClinicId && a.Name == request.Name, cancellationToken);

            if (nameExists)
            {
                return Result<int>.Conflict("AppointmentType.NameAlreadyExists",
                    "Appointment type with the same name already exists in this clinic.");
            }

            var appointmentType = new Domain.Entities.AppointmentType
            {
                ClinicId = request.ClinicId,
                Name = request.Name,
                DurationInMinutes = request.DurationInMinutes,
                Color = request.Color,
                AllowForPatientBooking = request.AllowForPatientBooking
            };

            var addedEntity = await dbContext.AppointmentTypes.AddAsync(appointmentType, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(addedEntity.Entity.Id);
        }
    }
}
