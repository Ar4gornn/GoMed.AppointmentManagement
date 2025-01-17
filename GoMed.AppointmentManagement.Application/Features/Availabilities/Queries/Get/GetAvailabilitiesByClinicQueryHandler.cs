using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Get.GetAvailabilitiesByClinic
{
    public class GetAvailabilitiesByClinicQueryHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService
    ) : IRequestHandler<Availabilities.Get.GetAvailabilitiesByClinic.GetAvailabilitiesByClinic, Result<List<Availability>>>
    {
        public async Task<Result<List<Availability>>> Handle(
            Availabilities.Get.GetAvailabilitiesByClinic.GetAvailabilitiesByClinic request,
            CancellationToken cancellationToken)
        {
            // Check if user can access this clinic
            if (!authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result<List<Availability>>.Unauthorized("Availability.Unauthorized",
                    "You do not have permission to view availabilities for this clinic.");
            }

            var results = await dbContext.Clinics.Where(e => e.Id == request.ClinicId).Select(c => c.Availabilities).FirstOrDefaultAsync();
            return results is null 
                ? Result<List<Availability>>.NotFound("Clinic.NotFound", "Clinic not found.") 
                : Result<List<Availability>>.Success(results);
        }
    }
}