using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentManagement.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Queries.GetAvailabilitiesByClinic;

public class GetAvailabilitiesByClinicQueryHandler(
    IApplicationDbContext dbContext) : IRequestHandler<GetAvailabilitiesByClinic, Result<List<AvailabilityDto>>>
{
    public async Task<Result<List<AvailabilityDto>>> Handle(Queries.GetAvailabilitiesByClinic.GetAvailabilitiesByClinic request, CancellationToken cancellationToken)
    {
        var results = await dbContext.Availabilities
            .Where(a => a.ClinicId == request.ClinicId)
            .Select(a => new AvailabilityDto
            {
                Id = a.Id,
                ClinicId = a.ClinicId ?? Guid.Empty,
                DayOfWeek = a.DayOfWeek,
                StartTime = a.StartTime,  // Directly assign DateTimeOffset
                EndTime   = a.EndTime     // Directly assign DateTimeOffset
            })
            .ToListAsync(cancellationToken);

        return Result<List<AvailabilityDto>>.Success(results);
    }

}