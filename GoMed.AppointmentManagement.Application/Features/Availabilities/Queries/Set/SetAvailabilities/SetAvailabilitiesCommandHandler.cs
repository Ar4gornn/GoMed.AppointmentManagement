using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Set.SetAvailabilities
{
    public class SetAvailabilitiesCommandHandler : IRequestHandler<SetAvailabilities, Result>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IAuthUserService _authUserService;

        public SetAvailabilitiesCommandHandler(IApplicationDbContext dbContext, IAuthUserService authUserService)
        {
            _dbContext = dbContext;
            _authUserService = authUserService;
        }

        public async Task<Result> Handle(SetAvailabilities request, CancellationToken cancellationToken)
        {
            // Check if user can access the target clinic
            if (!_authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result.Unauthorized("Availability.Unauthorized",
                    "You do not have permission to set availabilities for this clinic.");
            }

            // Validate the clinic exists along with its availabilities
            var clinic = await _dbContext.Clinics
                .Include(c => c.Availabilities)
                .FirstOrDefaultAsync(c => c.Id == request.ClinicId, cancellationToken);

            if (clinic is null)
            {
                return Result.NotFound("Clinic.NotFound", "Clinic not found.");
            }

            // Create a list of incoming availability IDs (if provided)
            var incomingIds = request.Availabilities
                .Where(dto => dto.Id.HasValue)
                .Select(dto => dto.Id.Value)
                .ToHashSet();

            // 1) Remove availabilities that exist in the clinic but are not present in the incoming list.
            var availabilitiesToRemove = clinic.Availabilities
                .Where(a => !incomingIds.Contains(a.Id))
                .ToList();

            foreach (var removeItem in availabilitiesToRemove)
            {
                clinic.Availabilities.Remove(removeItem);
            }

            // 2) Upsert (update or insert) incoming availabilities:
            foreach (var item in request.Availabilities)
            {
                if (item.Id.HasValue)
                {
                    var existing = clinic.Availabilities.FirstOrDefault(a => a.Id == item.Id.Value);
                    if (existing != null)
                    {
                        // Update
                        existing.DayOfWeek = item.DayOfWeek;
                        existing.StartTime = item.StartTime;
                        existing.EndTime = item.EndTime;
                    }
                    else
                    {
                        // Create new
                        var newAvailability = new Availability
                        {
                            Clinic = clinic,
                            DayOfWeek = item.DayOfWeek,
                            StartTime = item.StartTime,
                            EndTime = item.EndTime
                        };
                        clinic.Availabilities.Add(newAvailability);
                    }
                }
                else
                {
                    // Create new
                    var newAvailability = new Availability
                    {
                        Clinic = clinic,
                        DayOfWeek = item.DayOfWeek,
                        StartTime = item.StartTime,
                        EndTime = item.EndTime
                    };
                    clinic.Availabilities.Add(newAvailability);
                }
            }

            // 3) Persist changes
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
