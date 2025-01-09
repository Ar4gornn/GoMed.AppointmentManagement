using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByClinicIdQuery
{
    /// <summary>
    /// Query to retrieve appointments by clinic, within a date range.
    /// </summary>
    public record GetAppointmentByClinicIdQuery(Guid ClinicId, DateTimeOffset StartDate, DateTimeOffset EndDate) 
        : IRequest<List<ReadAppointmentDto>>;
}