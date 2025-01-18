using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Set
{
    public class SetAvailabilities : IRequest<Result>
    {
        public Guid ClinicId { get; init; }
        
        // A list of availabilities to set for this clinic.
        // Existing availabilities may be updated if their Id is specified;
        // new availabilities will be created if no Id is provided.
        public List<Availability> Availabilities { get; init; } = new();
    }

}