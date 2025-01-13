using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Availabilities.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Set.SetAvailabilities
{
    public class SetAvailabilities : IRequest<Result>
    {
        public Guid ClinicId { get; init; }
        
        // A list of availabilities to set for this clinic.
        // Existing availabilities may be updated if their Id is specified;
        // new availabilities will be created if no Id is provided.
        public List<CreateAvailabilityDto> Availabilities { get; init; } = new();
    }

}