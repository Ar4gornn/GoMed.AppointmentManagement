using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentManagements.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.Get.GetAppointmetnTypeById;
public class GetAppointmentTypeById : IRequest<Result<AppointmentTypeDto>>
{
    public int Id { get; init; }
}

public class GetAppointmentTypeByIdQueryHandler(
    IApplicationDbContext dbContext) 
    : IRequestHandler<GetAppointmentTypeById, Result<AppointmentTypeDto>>
{
    public async Task<Result<AppointmentTypeDto>> Handle(GetAppointmentTypeById request, CancellationToken cancellationToken)
    {
        var appointmentType = await dbContext.AppointmentTypes
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (appointmentType == null)
        {
            return Result<AppointmentTypeDto>.NotFound("AppointmentType.NotFound", "Appointment type does not exist.");
        }

        // Map entity to DTO
        var dto = new AppointmentTypeDto
        {
            Id = appointmentType.Id,
            ClinicId = appointmentType.ClinicId,
            Name = appointmentType.Name,
            DurationInMinutes = appointmentType.DurationInMinutes,
            Color = appointmentType.Color,
            AllowForPatientBooking = appointmentType.AllowForPatientBooking
        };

        return Result<AppointmentTypeDto>.Success(dto);
    }
}
