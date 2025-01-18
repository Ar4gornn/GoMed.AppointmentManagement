using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByClinicIdQuery
{
    public record GetAppointmentByClinicIdQuery(
        Guid ClinicId, 
        DateTimeOffset StartDate,
        DateTimeOffset EndDate
    ) : IRequest<Result<List<ReadAppointmentDto>>>;
}