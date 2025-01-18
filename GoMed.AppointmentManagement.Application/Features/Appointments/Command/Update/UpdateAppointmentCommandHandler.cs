using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Update
{
    public class UpdateAppointmentCommandHandler(
        IApplicationDbContext dbContext,
        IAuthUserService authUserService
    ) : IRequestHandler<UpdateAppointmentCommand, Result<ReadAppointmentDto>>
    {
        public async Task<Result<ReadAppointmentDto>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var appointment = await dbContext.Appointments.FindAsync(new object?[] { dto.Id }, cancellationToken);

            if (appointment == null)
            {
                return Result<ReadAppointmentDto>.NotFound("Appointment.NotFound", $"Appointment with Id {dto.Id} not found.");
            }

            if (!authUserService.CanAccessClinic(appointment.ClinicId))
            {
                return Result<ReadAppointmentDto>.Unauthorized("Appointment.Unauthorized", "You do not have permission to update this appointment.");
            }

            appointment.PatientId = Guid.Parse(dto.PatientId);
            appointment.PatientName = dto.PatientName;
            appointment.PatientPhone = dto.PatientPhone;
            appointment.Type = dto.Type;
            appointment.Notes = dto.Notes;
            appointment.StartAt = dto.NewStartTime;
            appointment.EndAt = dto.NewEndTime ?? dto.NewStartTime.AddMinutes(30);

            dbContext.Appointments.Update(appointment);
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
