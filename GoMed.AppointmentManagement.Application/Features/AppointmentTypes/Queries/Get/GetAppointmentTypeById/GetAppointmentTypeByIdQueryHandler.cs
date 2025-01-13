using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.Get.GetAppointmentTypeById
{
    public class GetAppointmentTypeByIdQueryHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService
    ) : IRequestHandler<GetAppointmentTypeById, Result<ReadAppointmentTypeDto>>
    {
        public async Task<Result<ReadAppointmentTypeDto>> Handle(GetAppointmentTypeById request, CancellationToken cancellationToken)
        {
            var appointmentType = await dbContext.AppointmentTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (appointmentType == null)
            {
                return Result<ReadAppointmentTypeDto>.NotFound("AppointmentType.NotFound", "Appointment type does not exist.");
            }

            // Check clinic access. If ClinicId is null, return Forbidden.
            if (!appointmentType.ClinicId.HasValue || !authUserService.CanAccessClinic(appointmentType.ClinicId.Value))
            {
                return Result<ReadAppointmentTypeDto>.Forbidden("AppointmentType.Forbidden",
                    "You do not have permission to view appointment types in this clinic.");
            }

            // Map entity to DTO
            var dto = new ReadAppointmentTypeDto
            {
                Id = appointmentType.Id,
                ClinicId = appointmentType.ClinicId.Value,
                Name = appointmentType.Name,
                DurationInMinutes = appointmentType.DurationInMinutes,
                Color = appointmentType.Color,
                AllowForPatientBooking = appointmentType.AllowForPatientBooking
            };

            return Result<ReadAppointmentTypeDto>.Success(dto);
        }
    }
}
