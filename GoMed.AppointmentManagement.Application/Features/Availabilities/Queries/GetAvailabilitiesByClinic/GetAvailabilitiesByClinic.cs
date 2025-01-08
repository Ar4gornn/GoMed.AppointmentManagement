using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentManagement.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Queries.GetAvailabilitiesByClinic;

public class GetAvailabilitiesByClinic : IRequest<Result<List<AvailabilityDto>>>
{
    public Guid ClinicId { get; init; }
}