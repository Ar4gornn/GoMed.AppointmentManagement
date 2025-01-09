using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Availabilities.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Queries.GetAvailabilitiesByClinic;

public class GetAvailabilitiesByClinicQueryHandler(
    IApplicationDbContext dbContext) : IRequestHandler<GetAvailabilitiesByClinic, Result<List<ReadAvailabilityDto>>>
{
    public async Task<Result<List<ReadAvailabilityDto>>> Handle(GetAvailabilitiesByClinic request, CancellationToken cancellationToken)
    {
        var results = await dbContext.Availabilities
            .Where(a => a.Clinic != null && a.Clinic.Id == request.ClinicId)  // Filter by ClinicId
            .Select(a => new ReadAvailabilityDto
            {
                ClinicId = a.Clinic!.Id,  // Map ClinicId from Clinic entity
                DayOfWeek = a.DayOfWeek,
                StartTime = a.StartTime,
                EndTime = a.EndTime
            })
            .ToListAsync(cancellationToken);

        return Result<List<ReadAvailabilityDto>>.Success(results);
    }
}