using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Delete.DeleteAvailability;

public class DeleteAvailabilityValidator : AbstractValidator<DeleteAvailability>
{
    public DeleteAvailabilityValidator()
    {
        RuleFor(x => x.AvailabilityId)
            .GreaterThan(0).WithMessage("Invalid AvailabilityId.");
    }
}