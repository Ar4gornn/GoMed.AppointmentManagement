using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByPatientIdQuery
{
    public class GetAppointmentByPatientIdQueryHandler : IRequestHandler<GetAppointmentByPatientIdQuery, List<ReadAppointmentDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IAuthUserService _authUserService;

        public GetAppointmentByPatientIdQueryHandler(IApplicationDbContext dbContext, IAuthUserService authUserService)
        {
            _dbContext = dbContext;
            _authUserService = authUserService;
        }

        public async Task<List<ReadAppointmentDto>> Handle(GetAppointmentByPatientIdQuery request, CancellationToken cancellationToken)
        {
            // Check patient access
            if (!_authUserService.CanAccessPatient(request.PatientId))
            {
                throw new UnauthorizedAccessException(
                    "You do not have permission to view this patient's appointments.");
            }

            var patientAppointments = await _dbContext.Appointments
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

            return patientAppointments;
        }
    }
}
