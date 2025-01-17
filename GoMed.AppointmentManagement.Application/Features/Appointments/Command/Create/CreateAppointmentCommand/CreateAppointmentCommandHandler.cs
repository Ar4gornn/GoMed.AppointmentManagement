using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Enums;
using GoMed.AppointmentManagement.Persistence;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, ReadAppointmentDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthUserService _authUserService;

        public CreateAppointmentCommandHandler(ApplicationDbContext dbContext, IAuthUserService authUserService)
        {
            _dbContext = dbContext;
            _authUserService = authUserService;
        }

        public async Task<ReadAppointmentDto> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // Check clinic access
            if (!_authUserService.CanAccessClinic(dto.ClinicId))
            {
                throw new UnauthorizedAccessException(
                    "You do not have permission to create an appointment in this clinic.");
            }

            // Optionally check patient access if you want to ensure the user 
            // can create an appointment for the specified patient
            // if (!_authUserService.CanAccessPatient(Guid.Parse(dto.PatientId))) 
            // {
            //     throw new UnauthorizedAccessException("You do not have permission to schedule for this patient.");
            // }

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
}
