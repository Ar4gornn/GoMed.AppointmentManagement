using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.GetAll.GetAllAppointmentTypesQueries
{
    public class GetAllAppointmentTypesQueryHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService
    ) : IRequestHandler<GetAllAppointmentTypes, Result<List<ReadAppointmentTypeDto>>>
    {
        public async Task<Result<List<ReadAppointmentTypeDto>>> Handle(GetAllAppointmentTypes request, CancellationToken cancellationToken)
        {
            // If filtering by a specific clinic, check access
            if (request.ClinicId != Guid.Empty || !authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result<List<ReadAppointmentTypeDto>>.Unauthorized("AppointmentType.Unauthorized",
                    "You do not have permission to view appointment types in this clinic.");
            }

            var list = await dbContext.AppointmentTypes.AsNoTracking()
                .Select(a => new ReadAppointmentTypeDto
                {
                    Id = a.Id,
                    ClinicId = a.ClinicId,
                    Name = a.Name,
                    DurationInMinutes = a.DurationInMinutes,
                    Color = a.Color,
                    AllowForPatientBooking = a.AllowForPatientBooking
                })
                .ToListAsync(cancellationToken);

            return Result<List<ReadAppointmentTypeDto>>.Success(list);
        }
    }
}
