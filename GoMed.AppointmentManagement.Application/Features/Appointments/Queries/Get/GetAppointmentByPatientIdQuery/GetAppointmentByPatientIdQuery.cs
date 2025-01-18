using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByPatientIdQuery
{
    public record GetAppointmentByPatientIdQuery(Guid PatientId)
        : IRequest<Result<List<ReadAppointmentDto>>>;
}