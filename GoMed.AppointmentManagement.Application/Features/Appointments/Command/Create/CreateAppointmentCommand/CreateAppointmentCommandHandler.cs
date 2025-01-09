using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, ReadAppointmentDto>
    {
        private readonly AppointmentDbContext _dbContext;

        public CreateAppointmentCommandHandler(AppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReadAppointmentDto> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var appointment = new Appointment
            {
                ProfessionalId = Guid.Parse(dto.ProfessionalId),
                ClinicId = dto.ClinicId,
                PatientId = Guid.Parse(dto.PatientId),
                PatientName = dto.PatientName,
                PatientPhone = dto.PatientPhone,
                StartAt = dto.StartAt,
                EndAt = dto.EndAt,
                Type = dto.Type,
                Notes = dto.Notes,
                Status = AppointmentStatus.Pending,
                ShowedUp = false,
                BookingChannel = BookingChannel.PatientBooking
            };

            _dbContext.Appointments.Add(appointment);
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

    public class AppointmentDbContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
    }
}
