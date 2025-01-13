using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Enums;
using GoMed.AppointmentManagement.Persistence;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Approve
{
    public class ApproveAppointmentCommandHandler : IRequestHandler<ApproveAppointmentCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthUserService _authUserService;

        public ApproveAppointmentCommandHandler(ApplicationDbContext dbContext, IAuthUserService authUserService)
        {
            _dbContext = dbContext;
            _authUserService = authUserService;
        }

        public async Task Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _dbContext.Appointments.FindAsync(
                new object?[] { request.AppointmentId },
                cancellationToken);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with Id {request.AppointmentId} not found.");
            }

            // Ensure the user can access this appointment’s clinic
            if (!_authUserService.CanAccessClinic(appointment.ClinicId))
            {
                throw new UnauthorizedAccessException(
                    "You do not have permission to approve an appointment in this clinic.");
            }

            appointment.Status = request.IsApproved ? AppointmentStatus.Confirmed : AppointmentStatus.Cancelled;

            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}