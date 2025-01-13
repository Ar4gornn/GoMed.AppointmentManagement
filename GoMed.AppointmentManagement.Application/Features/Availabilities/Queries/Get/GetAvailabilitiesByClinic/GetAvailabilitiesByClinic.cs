using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Availabilities.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Get.GetAvailabilitiesByClinic
{
    public class GetAvailabilitiesByClinic : IRequest<Result<List<ReadAvailabilityDto>>>
    {
        public Guid ClinicId { get; init; }
    }
}