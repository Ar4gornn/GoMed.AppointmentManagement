using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Create.CreateAvailability
{
    public class CreateAvailabilityValidator : AbstractValidator<CreateAvailability>
    {
        public CreateAvailabilityValidator()
        {
            RuleFor(x => x.ClinicId)
                .NotEmpty().WithMessage("ClinicId is required.");
            
            RuleFor(x => x.DayOfWeek)
                .InclusiveBetween(0, 6).WithMessage("DayOfWeek must be between 0 (Sunday) and 6 (Saturday).");
            
            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime).WithMessage("StartTime must be before EndTime.");
            
            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime.");
        }
    }
}