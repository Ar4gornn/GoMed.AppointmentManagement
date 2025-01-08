using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentManagements.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.GetAll.GetAllAppointmentTypesQueries;

public class GetAllAppointmentTypesQueryHandler(
    IApplicationDbContext dbContext) 
    : IRequestHandler<GetAllAppointmentTypes, Result<List<AppointmentTypeDto>>>
{
    public async Task<Result<List<AppointmentTypeDto>>> Handle(GetAllAppointmentTypes request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.AppointmentType> query = dbContext.AppointmentTypes.AsNoTracking();

        // If filtering by Clinic
        if (request.ClinicId.HasValue)
        {
            query = query.Where(a => a.ClinicId == request.ClinicId.Value);
        }

        var list = await query
            .Select(a => new AppointmentTypeDto
            {
                Id = a.Id,
                ClinicId = a.ClinicId,
                Name = a.Name,
                DurationInMinutes = a.DurationInMinutes,
                Color = a.Color,
                AllowForPatientBooking = a.AllowForPatientBooking
            })
            .ToListAsync(cancellationToken);

        return Result<List<AppointmentTypeDto>>.Success(list);
    }
}
