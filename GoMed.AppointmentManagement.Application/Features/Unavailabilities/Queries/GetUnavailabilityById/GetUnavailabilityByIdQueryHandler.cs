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

        public GetUnavailabilityByIdQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<ReadUnavailabilityDto>> Handle(Queries.GetUnavailabilityById.GetUnavailabilityById request, CancellationToken cancellationToken)
        {
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

                // If you still want to map to the separate StartTime/EndTime fields
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };

            return Result<ReadUnavailabilityDto>.Success(dto);
        }
    }
}