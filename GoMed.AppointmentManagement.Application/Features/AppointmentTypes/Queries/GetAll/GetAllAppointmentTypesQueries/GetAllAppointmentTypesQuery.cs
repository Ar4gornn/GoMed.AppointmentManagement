using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentManagements.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.GetAll.GetAllAppointmentTypesQueries;

public class GetAllAppointmentTypes : IRequest<Result<List<AppointmentTypeDto>>>
{
    public Guid? ClinicId { get; init; } // optional filter by Clinic
}

