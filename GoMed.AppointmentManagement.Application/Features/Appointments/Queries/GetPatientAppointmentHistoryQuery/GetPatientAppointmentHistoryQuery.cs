using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.GetPatientAppointmentHistoryQuery
{
    public record GetPatientAppointmentHistoryQuery(Guid PatientId, int Page)
        : IRequest<Result<List<Appointment>>>;
}