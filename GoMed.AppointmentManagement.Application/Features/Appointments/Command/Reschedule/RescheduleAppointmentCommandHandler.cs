using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Events;
using GoMed.AppointmentManagement.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Reschedule
{
    public class RescheduleAppointmentCommandHandler : IRequestHandler<RescheduleAppointmentCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public RescheduleAppointmentCommandHandler(ApplicationDbContext dbContext, IMediator mediator)
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

            await _mediator.Publish(new AppointmentRescheduledEvent(appointment), cancellationToken);
        }
    }
}