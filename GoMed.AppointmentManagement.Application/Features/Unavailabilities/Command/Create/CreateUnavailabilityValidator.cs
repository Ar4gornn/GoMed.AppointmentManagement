using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Create.CreateUnavailability
{
    public class CreateUnavailabilityValidator : AbstractValidator<CreateUnavailability>
    {
        public CreateUnavailabilityValidator()
        {
            RuleFor(x => x.ClinicId)
                .NotEmpty().WithMessage("ClinicId is required.");

            RuleFor(x => x.StartAt)
                .LessThan(x => x.EndAt)
                .WithMessage("StartDateTime must be before EndDateTime.");
            
            RuleFor(x => x.EndAt)
                .GreaterThan(x => x.StartAt)
                .WithMessage("EndDateTime must be after StartDateTime.");
            
        }
    }
}