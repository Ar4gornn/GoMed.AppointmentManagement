using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByClinicIdQuery
{
    public class GetAppointmentByClinicIdQueryHandler : IRequestHandler<GetAppointmentByClinicIdQuery, List<ReadAppointmentDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthUserService _authUserService;

        public GetAppointmentByClinicIdQueryHandler(ApplicationDbContext dbContext, IAuthUserService authUserService)
        {
            _dbContext = dbContext;
            _authUserService = authUserService;
        }

        public async Task<List<ReadAppointmentDto>> Handle(GetAppointmentByClinicIdQuery request, CancellationToken cancellationToken)
        {
            // Check clinic access
            if (!_authUserService.CanAccessClinic(request.ClinicId))
            {
                throw new UnauthorizedAccessException(
                    "You do not have permission to view appointments for this clinic.");
            }

            var appointments = await _dbContext.Appointments
                .Where(a => a.ClinicId == request.ClinicId &&
                            a.StartAt >= request.StartDate &&
                            a.EndAt <= request.EndDate)
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

            return appointments;
        }
    }
}
