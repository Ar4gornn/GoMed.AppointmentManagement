using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using GoMed.AppointmentManagement.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByClinicIdQuery
{
    public class GetAppointmentByClinicIdQueryHandler 
        : IRequestHandler<GetAppointmentByClinicIdQuery, List<ReadAppointmentDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAppointmentByClinicIdQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ReadAppointmentDto>> Handle(GetAppointmentByClinicIdQuery request, CancellationToken cancellationToken)
        {
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