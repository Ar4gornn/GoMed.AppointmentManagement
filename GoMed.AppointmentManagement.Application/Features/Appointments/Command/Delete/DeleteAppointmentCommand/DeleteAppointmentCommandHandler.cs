using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Command.Delete.DeleteAppointmentCommand
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand>
    {
        private readonly AppointmentDbContext _dbContext;

        public DeleteAppointmentCommandHandler(AppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _dbContext.Appointments.FindAsync(new object?[] { request.AppointmentId }, cancellationToken);

            if (appointment == null)
            {
                // Optionally ignore if not found or throw exception
                return;
            }

            _dbContext.Appointments.Remove(appointment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}