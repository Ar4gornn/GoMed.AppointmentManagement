using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Availabilities.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Get.GetAvailabilitiesByClinic
{
    public class GetAvailabilitiesByClinicQueryHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService
    ) : IRequestHandler<Availabilities.Get.GetAvailabilitiesByClinic.GetAvailabilitiesByClinic, Result<List<ReadAvailabilityDto>>>
    {
        public async Task<Result<List<ReadAvailabilityDto>>> Handle(
            Availabilities.Get.GetAvailabilitiesByClinic.GetAvailabilitiesByClinic request,
            CancellationToken cancellationToken)
        {
            // Check if user can access this clinic
            if (!authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result<List<ReadAvailabilityDto>>.Forbidden("Availability.Forbidden",
                    "You do not have permission to view availabilities for this clinic.");
            }

            var results = await dbContext.Availabilities
                .Where(a => a.Clinic != null && a.Clinic.Id == request.ClinicId)
                .Select(a => new ReadAvailabilityDto
                {
                    Id = a.Id,
                    ClinicId = a.Clinic!.Id,
                    DayOfWeek = a.DayOfWeek,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                })
                .ToListAsync(cancellationToken);

            return Result<List<ReadAvailabilityDto>>.Success(results);
        }
    }
}