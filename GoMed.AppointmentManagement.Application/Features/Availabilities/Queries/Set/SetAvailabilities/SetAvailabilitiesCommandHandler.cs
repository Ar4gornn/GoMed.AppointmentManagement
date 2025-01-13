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

        public SetAvailabilitiesCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetAvailabilities request, CancellationToken cancellationToken)
        {
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
            // This removes availabilities that the client has omitted.
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
                    // If an ID is provided, try to find the existing availability.
                    var existing = clinic.Availabilities.FirstOrDefault(a => a.Id == item.Id.Value);
                    if (existing != null)
                    {
                        // Update the existing availability's properties.
                        existing.DayOfWeek = item.DayOfWeek;
                        existing.StartTime = item.StartTime;
                        existing.EndTime = item.EndTime;
                    }
                    else
                    {
                        // If the provided ID is not found, create a new availability.
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
                    // No ID provided: create a new availability.
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

            // 3) Persist the changes
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
