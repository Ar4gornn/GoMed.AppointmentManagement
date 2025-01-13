using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events.Unavailability;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Delete.DeleteUnavailability
{
    public class DeleteUnavailabilityCommandHandler : IRequestHandler<DeleteUnavailability, Result>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMediator _mediator;
        private readonly IAuthUserService _authUserService;

        public DeleteUnavailabilityCommandHandler(
            IApplicationDbContext dbContext,
            IPublishEndpoint publishEndpoint,
            IMediator mediator,
            IAuthUserService authUserService
        )
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
            _mediator = mediator;
            _authUserService = authUserService;
        }

        public async Task<Result> Handle(DeleteUnavailability request, CancellationToken cancellationToken)
        {
            // Check clinic access
            if (!_authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result.Forbidden("Unavailability.Forbidden",
                    "You do not have permission to delete unavailability for this clinic.");
            }

            var unavailability = await _dbContext.Unavailabilities
                .FirstOrDefaultAsync(
                    u => u.Id == request.Id && u.ClinicId == request.ClinicId,
                    cancellationToken);

            if (unavailability is null)
            {
                return Result.NotFound("Unavailability.NotFound", "Unavailability not found.");
            }

            _dbContext.Unavailabilities.Remove(unavailability);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Publish domain event
            var @event = new UnavailabilityDeletedEvent(request.Id, request.ClinicId);
            await _publishEndpoint.Publish(@event, cancellationToken);
            await _mediator.Publish(@event, cancellationToken);

            return Result.Success();
        }
    }
}
