using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.GetAll.GetAllAppointmentTypesQueries;

public class GetAllAppointmentTypesQueryHandler(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetAllAppointmentTypes, Result<List<ReadAppointmentTypeDto>>>
{
    public async Task<Result<List<ReadAppointmentTypeDto>>> Handle(GetAllAppointmentTypes request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.AppointmentType> query = dbContext.AppointmentTypes.AsNoTracking();

        // If filtering by Clinic
        if (request.ClinicId != Guid.Empty)
        {
            query = query.Where(a => a.ClinicId == request.ClinicId);
        }

        var list = await query
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