using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByPatientIdQuery
{
    public class GetAppointmentByPatientIdQueryHandler : IRequestHandler<GetAppointmentByPatientIdQuery, List<ReadAppointmentDto>>
    {
        private readonly AppointmentDbContext _dbContext;

        public GetAppointmentByPatientIdQueryHandler(AppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ReadAppointmentDto>> Handle(GetAppointmentByPatientIdQuery request, CancellationToken cancellationToken)
        {
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