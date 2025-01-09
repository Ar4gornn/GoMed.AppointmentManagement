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

        public DeleteUnavailabilityCommandHandler(
            IApplicationDbContext dbContext,
            IPublishEndpoint publishEndpoint,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
            _mediator = mediator;
        }

        public async Task<Result> Handle(DeleteUnavailability request, CancellationToken cancellationToken)
        {
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