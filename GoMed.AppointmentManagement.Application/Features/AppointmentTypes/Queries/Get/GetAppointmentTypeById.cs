using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.Get;
public class GetAppointmentTypeById : IRequest<Result<ReadAppointmentTypeDto>>
{
    public int Id { get; init; }
    public Guid ClinicId { get; init; }
}
