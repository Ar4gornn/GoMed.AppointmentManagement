using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand;
using GoMed.AppointmentManagement.Domain.Enums;
using GoMed.AppointmentManagement.Domain.Events;
using GoMed.AppointmentManagement.Persistence;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Cancel
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator; // If you want to publish domain events

        public CancelAppointmentCommandHandler(ApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var appointment = await _dbContext.Appointments.FindAsync(new object?[] { dto.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with Id {dto.AppointmentId} not found.");
            }

            appointment.Status = AppointmentStatus.Cancelled;

            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new AppointmentCancelledEvent(appointment), cancellationToken);
        }
    }
}