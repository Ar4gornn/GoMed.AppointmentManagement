using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand;
using GoMed.AppointmentManagement.Domain.Enums;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Approve
{
    public class ApproveAppointmentCommandHandler : IRequestHandler<ApproveAppointmentCommand>
    {
        private readonly AppointmentDbContext _dbContext;

        public ApproveAppointmentCommandHandler(AppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _dbContext.Appointments.FindAsync(new object?[] { request.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with Id {request.AppointmentId} not found.");
            }

            appointment.Status = request.IsApproved ? AppointmentStatus.Confirmed : AppointmentStatus.Cancelled;

            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}