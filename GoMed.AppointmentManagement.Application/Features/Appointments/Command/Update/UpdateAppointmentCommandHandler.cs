using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using GoMed.AppointmentManagement.Persistence;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Update
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, ReadAppointmentDto>
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateAppointmentCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReadAppointmentDto> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var appointment = await _dbContext.Appointments.FindAsync(new object?[] { dto.Id }, cancellationToken);

            if (appointment == null)
            {
                // Handle not found, throw exception or return null
                throw new KeyNotFoundException($"Appointment with Id {dto.Id} not found.");
            }

            appointment.PatientId = Guid.Parse(dto.PatientId);
            appointment.PatientName = dto.PatientName;
            appointment.PatientPhone = dto.PatientPhone;
            appointment.Type = dto.Type;
            appointment.Notes = dto.Notes;
            appointment.StartAt = dto.NewStartTime;
            appointment.EndAt = dto.NewEndTime ?? dto.NewStartTime.AddMinutes(30); 
            // Some fallback logic, e.g., default duration if EndTime is null

            // Mark as modified explicitly or let EF detect changes
            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ReadAppointmentDto
            {
                ProfessionalId = appointment.ProfessionalId,
                ClinicId = appointment.ClinicId,
                PatientId = appointment.PatientId,
                PatientName = appointment.PatientName,
                PatientPhone = appointment.PatientPhone,
                StartAt = appointment.StartAt,
                EndAt = appointment.EndAt,
                Type = appointment.Type,
                Notes = appointment.Notes,
                ShowedUp = appointment.ShowedUp
            };
        }
    }
}
