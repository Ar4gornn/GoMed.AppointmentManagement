using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Update
{
    public class UpdateAppointmentTypeCommandHandler(
        IApplicationDbContext dbContext,
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
                return Result<int>.Unauthorized("AppointmentType.Unauthorized",
                    "You do not have permission to update appointment types in this clinic.");
            }
            

            // // Check if a different appointment type with the same name exists in the target clinic
            // bool nameExists = await dbContext.AppointmentTypes
            //     .AnyAsync(a =>
            //             a.Id != request.Id &&
            //             a.ClinicId == request.ClinicId &&
            //             a.Name == request.Name,
            //         cancellationToken);
            //
            // if (nameExists)
            // {
            //     return Result<int>.Conflict("AppointmentType.NameConflict",
            //         "Another appointment type with this name already exists in the clinic.");
            // }

            // Update fields
            appointmentType.Name = request.Name;
            appointmentType.DurationInMinutes = request.DurationInMinutes;
            appointmentType.Color = request.Color;
            appointmentType.AllowForPatientBooking = request.AllowForPatientBooking;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(appointmentType.Id);
        }
    }
}
