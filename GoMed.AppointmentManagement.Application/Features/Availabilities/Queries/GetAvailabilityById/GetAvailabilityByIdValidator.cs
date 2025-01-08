using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Queries;

public class GetAvailabilityByIdValidator : AbstractValidator<GetAvailabilityById.GetAvailabilityById>
{
    public GetAvailabilityByIdValidator()
    {
        RuleFor(x => x.AvailabilityId)
            .GreaterThan(0).WithMessage("Invalid AvailabilityId.");
    }
}