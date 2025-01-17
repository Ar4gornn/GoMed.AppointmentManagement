using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Enums;
using GoMed.AppointmentManagement.Domain.Events;

using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Cancel
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly IAuthUserService _authUserService;

        public CancelAppointmentCommandHandler(
            IApplicationDbContext dbContext,
            IMediator mediator,
            IAuthUserService authUserService)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _authUserService = authUserService;
        }

        public async Task Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var appointment = await _dbContext.Appointments
                .FindAsync(new object?[] { dto.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with Id {dto.AppointmentId} not found.");
            }

            // Check clinic access
            if (!_authUserService.CanAccessClinic(appointment.ClinicId))
            {
                throw new UnauthorizedAccessException(
                    "You do not have permission to cancel an appointment in this clinic.");
            }

            appointment.Status = AppointmentStatus.Cancelled;
            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new AppointmentCancelledEvent(appointment), cancellationToken);
        }
    }
}