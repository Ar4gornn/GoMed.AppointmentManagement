using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events;

using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Reschedule
{
    public class RescheduleAppointmentCommandHandler : IRequestHandler<RescheduleAppointmentCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly IAuthUserService _authUserService;

        public RescheduleAppointmentCommandHandler(
            IApplicationDbContext dbContext,
            IMediator mediator,
            IAuthUserService authUserService)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _authUserService = authUserService;
        }

        public async Task Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _dbContext.Appointments.FindAsync(
                new object?[] { request.AppointmentId },
                cancellationToken);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with Id {request.AppointmentId} not found.");
            }

            // Check clinic access
            if (!_authUserService.CanAccessClinic(appointment.ClinicId))
            {
                throw new UnauthorizedAccessException(
                    "You do not have permission to reschedule an appointment in this clinic.");
            }

            appointment.StartAt = request.StartAt;
            appointment.EndAt = request.EndAt;

            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new AppointmentRescheduledEvent(appointment), cancellationToken);
        }
    }
}
