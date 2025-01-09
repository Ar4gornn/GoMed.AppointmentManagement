using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand;
using GoMed.AppointmentManagement.Domain.Events;
using GoMed.AppointmentManagement.Persistence;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Set.SetAppointmentShowedUpCommand
{
    public class SetAppointmentShowedUpCommandHandler : IRequestHandler<Command.Set.SetAppointmentShowedUpCommand.SetAppointmentShowedUpCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public SetAppointmentShowedUpCommandHandler(ApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(Command.Set.SetAppointmentShowedUpCommand.SetAppointmentShowedUpCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _dbContext.Appointments.FindAsync(new object?[] { request.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with Id {request.AppointmentId} not found.");
            }

            appointment.ShowedUp = request.ShowedUp;

            // Optionally set status to Completed if they showed up?
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