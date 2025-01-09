using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Events;
using GoMed.AppointmentManagement.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Set.SetAppointmentShowedUpCommand
{
    public class SetAppointmentShowedUpCommandHandler : IRequestHandler<SetAppointmentShowedUpCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public SetAppointmentShowedUpCommandHandler(ApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(SetAppointmentShowedUpCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _dbContext.Appointments.FindAsync(new object?[] { request.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with Id {request.AppointmentId} not found.");
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