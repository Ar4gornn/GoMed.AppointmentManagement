using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Create
{
    public class CreateUnavailabilityCommandHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService)
        : IRequestHandler<CreateUnavailability, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateUnavailability request, CancellationToken cancellationToken)
        {
            // First, ensure user can access the specified clinic
            if (!authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result<int>.Unauthorized("Unavailability.Unauthorized",
                    "You do not have permission to create unavailability for this clinic.");
            }
            
            if (!await dbContext.Clinics
                    .AnyAsync(c => c.Id == request.ClinicId, cancellationToken))
            {
                return Result<int>.NotFound("Clinic.NotFound", "Clinic not found.");
            }

            // Create new unavailability
            var unavailability = new Unavailability
            {
                ClinicId = request.ClinicId,
                StartAt = request.StartAt,
                EndAt = request.EndAt,
                IsAllDay = request.IsAllDay
            };

            dbContext.Unavailabilities.Add(unavailability);
            await dbContext.SaveChangesAsync(cancellationToken);
            
            return Result<int>.Success(unavailability.Id);
        }
    }
}
