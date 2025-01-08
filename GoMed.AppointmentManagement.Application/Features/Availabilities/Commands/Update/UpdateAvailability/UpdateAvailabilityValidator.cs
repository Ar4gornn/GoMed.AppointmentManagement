using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Update.UpdateAvailability;

public class UpdateAvailabilityValidator : AbstractValidator<UpdateAvailability>
{
    public UpdateAvailabilityValidator()
    {
        RuleFor(x => x.AvailabilityId)
            .GreaterThan(0).WithMessage("Invalid AvailabilityId.");

        RuleFor(x => x.DayOfWeek)
            .InclusiveBetween(0, 6).WithMessage("DayOfWeek must be between 0 and 6.");
        
        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime).WithMessage("StartTime must be earlier than EndTime.");
    }
}