using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Events.Unavailability;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Update.UpdateUnavailability
{
    public class UpdateUnavailabilityCommandHandler : IRequestHandler<UpdateUnavailability, Result>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMediator _mediator;
        private readonly IAuthUserService _authUserService;

        public UpdateUnavailabilityCommandHandler(
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

        public async Task<Result> Handle(UpdateUnavailability request, CancellationToken cancellationToken)
        {
            // Check clinic access
            if (!_authUserService.CanAccessClinic(request.ClinicId))
            {
                return Result.Unauthorized("Unavailability.Unauthorized",
                    "You do not have permission to update unavailability for this clinic.");
            }

            // Find existing unavailability
            var unavailability = await _dbContext.Unavailabilities
                .FirstOrDefaultAsync(
                    u => u.Id == request.Id && u.ClinicId == request.ClinicId,
                    cancellationToken);

            if (unavailability is null)
            {
                return Result.NotFound("Unavailability.NotFound", "Unavailability not found.");
            }

            // Update fields
            unavailability.StartTime = request.StartDateTime;
            unavailability.EndTime = request.EndDateTime;
            unavailability.IsAllDay = request.IsAllDay;

            await _dbContext.SaveChangesAsync(cancellationToken);

            // Publish domain event
            var @event = new UnavailabilityUpdatedEvent(unavailability);
            await _publishEndpoint.Publish(@event, cancellationToken);
            await _mediator.Publish(@event, cancellationToken);

            return Result.Success();
        }
    }
}
