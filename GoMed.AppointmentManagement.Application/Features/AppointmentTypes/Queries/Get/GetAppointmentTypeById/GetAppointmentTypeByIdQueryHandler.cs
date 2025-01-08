using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.Get.GetAppointmentTypeById;

public class GetAppointmentTypeByIdQueryHandler(
    IApplicationDbContext dbContext) 
    : IRequestHandler<GetAppointmentTypeById, Result<ReadAppointmentTypeDto>>
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

        // Map entity to DTO
        var dto = new ReadAppointmentTypeDto
        {
            Id = appointmentType.Id,
            ClinicId = appointmentType.ClinicId,
            Name = appointmentType.Name,
            DurationInMinutes = appointmentType.DurationInMinutes,
            Color = appointmentType.Color,
            AllowForPatientBooking = appointmentType.AllowForPatientBooking
        };

        return Result<ReadAppointmentTypeDto>.Success(dto);
    }
}