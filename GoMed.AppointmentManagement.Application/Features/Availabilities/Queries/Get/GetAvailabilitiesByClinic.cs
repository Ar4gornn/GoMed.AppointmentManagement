using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Get.GetAvailabilitiesByClinic
{
    public class GetAvailabilitiesByClinic : IRequest<Result<List<Availability>>>
    {
        public Guid ClinicId { get; init; }
    }
}