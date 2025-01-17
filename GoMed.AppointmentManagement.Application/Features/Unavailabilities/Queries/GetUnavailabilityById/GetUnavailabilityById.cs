using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Queries.GetUnavailabilityById
{
    public class GetUnavailabilityById : IRequest<Result<ReadUnavailabilityDto>>
    {
        public int Id { get; set; }
        public Guid ClinicId { get; set; }
    }
}