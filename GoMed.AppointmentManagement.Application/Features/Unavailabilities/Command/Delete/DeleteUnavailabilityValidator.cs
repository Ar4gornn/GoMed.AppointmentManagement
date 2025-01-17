using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Delete.DeleteUnavailability
{
    public class DeleteUnavailabilityValidator : AbstractValidator<DeleteUnavailability>
    {
        public DeleteUnavailabilityValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Unavailability Id must be greater than 0.");

            RuleFor(x => x.ClinicId)
                .NotEmpty().WithMessage("ClinicId is required.");
        }
    }
}