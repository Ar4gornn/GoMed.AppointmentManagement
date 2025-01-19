using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Queries.GetUnavailabilitiesByClinic
{
    public class GetUnavailabilitiesByClinic : IRequest<Result<List<ReadUnavailabilityDto>>>
    {
        public Guid ClinicId { get; set; }
        
        //TODO: Add From To
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }
    }
}