using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Events.Unavailability;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Create.CreateUnavailability
{
    public class CreateUnavailabilityCommandHandler : IRequestHandler<CreateUnavailability, Result<int>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMediator _mediator;

        public CreateUnavailabilityCommandHandler(
            IApplicationDbContext dbContext,
            IPublishEndpoint publishEndpoint,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
            _mediator = mediator;
        }

        public async Task<Result<int>> Handle(CreateUnavailability request, CancellationToken cancellationToken)
        {
            // Validate clinic exists
            var clinic = await _dbContext.Clinics
                .FirstOrDefaultAsync(c => c.Id == request.ClinicId, cancellationToken);

            if (clinic is null)
            {
                return Result<int>.NotFound("Clinic.NotFound", "Clinic not found.");
            }

            // Create new unavailability
            var unavailability = new Unavailability
            {
                ClinicId = request.ClinicId,
                StartTime = request.StartDateTime,
                EndTime = request.EndDateTime,
                IsAllDay = request.IsAllDay
            };

            _dbContext.Unavailabilities.Add(unavailability);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Publish domain event
            var @event = new UnavailabilityCreatedEvent(unavailability);
            await _publishEndpoint.Publish(@event, cancellationToken);
            await _mediator.Publish(@event, cancellationToken);

            return Result<int>.Success(unavailability.Id);
        }
    }
}
