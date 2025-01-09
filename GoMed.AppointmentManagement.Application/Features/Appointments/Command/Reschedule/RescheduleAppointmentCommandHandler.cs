using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand;
using GoMed.AppointmentManagement.Domain.Events;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Reschedule
{
    public class RescheduleAppointmentCommandHandler : IRequestHandler<RescheduleAppointmentCommand>
    {
        private readonly AppointmentDbContext _dbContext;
        private readonly IMediator _mediator;

        public RescheduleAppointmentCommandHandler(AppointmentDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _dbContext.Appointments.FindAsync(new object?[] { request.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with Id {request.AppointmentId} not found.");
            }

            appointment.StartAt = request.StartAt;
            appointment.EndAt = request.EndAt;

            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Publish domain event
            await _mediator.Publish(new AppointmentRescheduledEvent(appointment), cancellationToken);
        }
    }
}