using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Queries.GetUnavailabilitiesByClinic
{
    public class GetUnavailabilitiesByClinicQueryHandler : IRequestHandler<GetUnavailabilitiesByClinic, Result<List<ReadUnavailabilityDto>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUnavailabilitiesByClinicQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<ReadUnavailabilityDto>>> Handle(GetUnavailabilitiesByClinic request, CancellationToken cancellationToken)
        {
            var results = await _dbContext.Unavailabilities
                .AsNoTracking()
                .Where(u => u.ClinicId == request.ClinicId)
                .Select(u => new ReadUnavailabilityDto
                {
                    Id = u.Id,
                    ClinicId = u.ClinicId ?? Guid.Empty,
                    StartDateTime = u.StartTime,
                    EndDateTime = u.EndTime,
                    IsAllDay = u.IsAllDay,
                    StartTime = u.StartTime,
                    EndTime = u.EndTime
                })
                .ToListAsync(cancellationToken);

            return Result<List<ReadUnavailabilityDto>>.Success(results);
        }
    }
}