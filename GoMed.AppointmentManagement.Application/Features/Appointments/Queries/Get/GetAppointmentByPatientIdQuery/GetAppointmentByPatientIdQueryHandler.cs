using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByPatientIdQuery
{
    public class GetAppointmentByPatientIdQueryHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService
    ) : IRequestHandler<GetAppointmentByPatientIdQuery, Result<List<ReadAppointmentDto>>>
    {
        public async Task<Result<List<ReadAppointmentDto>>> Handle(
            GetAppointmentByPatientIdQuery request,
            CancellationToken cancellationToken)
        {
            // Check patient access
            if (!authUserService.CanAccessPatient(request.PatientId))
            {
                return Result<List<ReadAppointmentDto>>.Unauthorized(
                    "Appointment.Unauthorized",
                    "You do not have permission to view this patient's appointments."
                );
            }

            var patientAppointments = await dbContext.Appointments
                .Where(a => a.PatientId == request.PatientId)
                .Select(a => new ReadAppointmentDto
                {
                    ProfessionalId = a.ProfessionalId,
                    ClinicId = a.ClinicId,
                    PatientId = a.PatientId,
                    PatientName = a.PatientName,
                    PatientPhone = a.PatientPhone,
                    StartAt = a.StartAt,
                    EndAt = a.EndAt,
                    Type = a.Type,
                    Notes = a.Notes,
                    ShowedUp = a.ShowedUp
                })
                .ToListAsync(cancellationToken);

            return Result<List<ReadAppointmentDto>>.Success(patientAppointments);
        }
    }
}