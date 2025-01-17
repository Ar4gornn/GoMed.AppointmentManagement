using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events;

using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Set.SetAppointmentShowedUpCommand
{
    public class SetAppointmentShowedUpCommandHandler : IRequestHandler<SetAppointmentShowedUpCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly IAuthUserService _authUserService;

        public SetAppointmentShowedUpCommandHandler(
            IApplicationDbContext dbContext,
            IMediator mediator,
            IAuthUserService authUserService)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _authUserService = authUserService;
        }

        public async Task Handle(SetAppointmentShowedUpCommand request, CancellationToken cancellationToken)
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
                    "You do not have permission to set showed-up status for an appointment in this clinic.");
            }

            appointment.ShowedUp = request.ShowedUp;

            if (request.ShowedUp)
            {
                appointment.Status = Domain.Enums.AppointmentStatus.Completed;
                await _mediator.Publish(new AppointmentCheckOutEvent(appointment), cancellationToken);
            }
            else
            {
                await _mediator.Publish(new AppointmentNoShowEvent(appointment), cancellationToken);
            }

            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
