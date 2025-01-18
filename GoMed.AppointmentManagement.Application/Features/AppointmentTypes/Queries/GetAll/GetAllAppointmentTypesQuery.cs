using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.GetAll;

public class GetAllAppointmentTypes : IRequest<Result<List<ReadAppointmentTypeDto>>>
{
    public Guid ClinicId { get; init; }
}