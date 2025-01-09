using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Create.CreateUnavailability
{
    public class CreateUnavailabilityValidator : AbstractValidator<CreateUnavailability>
    {
        public CreateUnavailabilityValidator()
        {
            RuleFor(x => x.ClinicId)
                .NotEmpty().WithMessage("ClinicId is required.");

            RuleFor(x => x.StartDateTime)
                .LessThan(x => x.EndDateTime)
                .WithMessage("StartDateTime must be before EndDateTime.");

            // If "IsAllDay" implies ignoring the times, that’s an app-level rule; 
            // you can add additional checks if needed.
        }
    }
}