using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Enums;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand
{
    public class CreateAppointmentCommandHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService
    ) : IRequestHandler<CreateAppointmentCommand, Result<ReadAppointmentDto>>
    {
        public async Task<Result<ReadAppointmentDto>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            if (!authUserService.CanAccessClinic(dto.ClinicId))
            {
                return Result<ReadAppointmentDto>.Unauthorized("Appointment.Unauthorized", "You do not have permission to create this appointment.");
            }

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

            dbContext.Appointments.Add(appointment);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result<ReadAppointmentDto>.Success(new ReadAppointmentDto
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
            });
        }
    }
}
