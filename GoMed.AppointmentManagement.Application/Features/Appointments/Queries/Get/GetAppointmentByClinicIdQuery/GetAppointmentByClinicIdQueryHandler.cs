using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByClinicIdQuery
{
    public class GetAppointmentByClinicIdQueryHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService
    ) : IRequestHandler<GetAppointmentByClinicIdQuery, Result<List<ReadAppointmentDto>>>
    {
        public async Task<Result<List<ReadAppointmentDto>>> Handle(
            GetAppointmentByClinicIdQuery request,
            CancellationToken cancellationToken)
        {
            // Check clinic access
            if (!authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result<List<ReadAppointmentDto>>.Unauthorized(
                    "Appointment.Unauthorized",
                    "You do not have permission to view appointments for this clinic."
                );
            }

            var appointments = await dbContext.Appointments
                .Where(a => a.ClinicId == request.ClinicId
                            && a.StartAt >= request.StartDate
                            && a.EndAt <= request.EndDate)
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

            return Result<List<ReadAppointmentDto>>.Success(appointments);
        }
    }
}
