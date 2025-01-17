using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Update.UpdateUnavailability
{
    public class UpdateUnavailabilityValidator : AbstractValidator<UpdateUnavailability>
    {
        public UpdateUnavailabilityValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Unavailability Id must be greater than 0.");

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