using GoMed.AppointmentManagement.Contracts.Interfaces;

using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Delete.DeleteAppointmentCommand
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IAuthUserService _authUserService;

        public DeleteAppointmentCommandHandler(IApplicationDbContext dbContext, IAuthUserService authUserService)
        {
            _dbContext = dbContext;
            _authUserService = authUserService;
        }

        public async Task Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _dbContext.Appointments.FindAsync(
                new object?[] { request.AppointmentId },
                cancellationToken);

            if (appointment == null)
            {
                // Optionally ignore if not found or throw exception
                return;
            }

            // Check clinic access
            if (!_authUserService.CanAccessClinic(appointment.ClinicId))
            {
                throw new UnauthorizedAccessException(
                    "You do not have permission to delete an appointment in this clinic.");
            }

            _dbContext.Appointments.Remove(appointment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}