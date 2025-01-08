using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentManagement.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Queries.GetAvailabilityById;

public class GetAvailabilityById : IRequest<Result<AvailabilityDto>>
{
    public int AvailabilityId { get; init; }
}