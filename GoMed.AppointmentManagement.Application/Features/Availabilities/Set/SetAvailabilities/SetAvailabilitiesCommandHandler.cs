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
            // Validate the clinic exists
            var clinic = await _dbContext.Clinics
                .Include(c => c.Availabilities)
                .FirstOrDefaultAsync(c => c.Id == request.ClinicId, cancellationToken);

            if (clinic is null)
            {
                return Result.NotFound("Clinic.NotFound", "Clinic not found.");
            }

            // 1) Clear existing Availabilities for this clinic
            clinic.Availabilities.Clear();

            // 2) Create new Availabilities and add them
            foreach (var item in request.Availabilities)
            {
                var newAvailability = new Availability
                {
                    Clinic = clinic,
                    DayOfWeek = item.DayOfWeek,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime
                };
                clinic.Availabilities.Add(newAvailability);
            }

            // 3) Save changes
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}