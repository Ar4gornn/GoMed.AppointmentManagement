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
        private readonly IAuthUserService _authUserService;

        public GetUnavailabilitiesByClinicQueryHandler(
            IApplicationDbContext dbContext,
            IAuthUserService authUserService
        )
        {
            _dbContext = dbContext;
            _authUserService = authUserService;
        }

        public async Task<Result<List<ReadUnavailabilityDto>>> Handle(GetUnavailabilitiesByClinic request, CancellationToken cancellationToken)
        {
            // Check clinic access
            if (!_authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result<List<ReadUnavailabilityDto>>.Unauthorized("Unavailability.Unauthorized",
                    "You do not have permission to view unavailabilities for this clinic.");
            }

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
