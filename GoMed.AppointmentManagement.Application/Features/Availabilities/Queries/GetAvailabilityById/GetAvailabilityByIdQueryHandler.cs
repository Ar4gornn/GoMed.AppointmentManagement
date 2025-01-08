using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentManagement.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Queries.GetAvailabilityById;

public class GetAvailabilityByIdQueryHandler(
    IApplicationDbContext dbContext) : IRequestHandler<GetAvailabilityById, Result<AvailabilityDto>>
{
    public async Task<Result<AvailabilityDto>> Handle(Queries.GetAvailabilityById.GetAvailabilityById request, CancellationToken cancellationToken)
    {
        // Query the database to find an availability record by its ID
        var availability = await dbContext.Availabilities
            .Where(a => a.Id == request.AvailabilityId)  // Filter the record by the requested AvailabilityId
            .Select(a => new AvailabilityDto  // Project the result into an AvailabilityDto
            {
                Id = a.Id,  
                ClinicId = a.ClinicId ?? Guid.Empty,  
                DayOfWeek = a.DayOfWeek,  
                StartTime = a.StartTime,  
                EndTime   = a.EndTime     
            })
            .FirstOrDefaultAsync(cancellationToken);  // Return the first matching record or null if not found

        // If no availability record is found, return a NotFound result
        if (availability is null)
            return Result<AvailabilityDto>.NotFound("Availability.NotFound", "Availability not found.");

        // If the record is found, return it wrapped in a Success result
        return Result<AvailabilityDto>.Success(availability);
    }


}

