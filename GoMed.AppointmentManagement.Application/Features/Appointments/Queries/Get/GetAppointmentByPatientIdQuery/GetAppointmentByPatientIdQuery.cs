using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByPatientIdQuery
{
    /// <summary>
    /// Query to retrieve appointments by PatientId.
    /// </summary>
    public record GetAppointmentByPatientIdQuery(Guid PatientId) : IRequest<List<ReadAppointmentDto>>;
}