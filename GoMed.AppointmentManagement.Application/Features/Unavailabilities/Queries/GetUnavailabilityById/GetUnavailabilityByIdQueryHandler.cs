using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Queries.GetUnavailabilityById
{
    public class GetUnavailabilityByIdQueryHandler : IRequestHandler<GetUnavailabilityById, Result<ReadUnavailabilityDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IAuthUserService _authUserService;

        public GetUnavailabilityByIdQueryHandler(
            IApplicationDbContext dbContext,
            IAuthUserService authUserService
        )
        {
            _dbContext = dbContext;
            _authUserService = authUserService;
        }

        public async Task<Result<ReadUnavailabilityDto>> Handle(GetUnavailabilityById request, CancellationToken cancellationToken)
        {
            // Check clinic access
            if (!_authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result<ReadUnavailabilityDto>.Unauthorized("Unavailability.Unauthorized",
                    "You do not have permission to view unavailability for this clinic.");
            }

            var entity = await _dbContext.Unavailabilities
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    u => u.Id == request.Id && u.ClinicId == request.ClinicId,
                    cancellationToken);

            if (entity is null)
            {
                return Result<ReadUnavailabilityDto>.NotFound("Unavailability.NotFound", "Unavailability not found.");
            }

            var dto = new ReadUnavailabilityDto
            {
                Id = entity.Id,
                ClinicId = entity.ClinicId ?? Guid.Empty,
                StartDateTime = entity.StartTime,
                EndDateTime = entity.EndTime,
                IsAllDay = entity.IsAllDay,

                // Mapping these again if you want them separately
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };

            return Result<ReadUnavailabilityDto>.Success(dto);
        }
    }
}
