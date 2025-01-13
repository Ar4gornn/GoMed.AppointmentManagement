using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Update.UpdateAvailability
{
    public class UpdateAvailabilityValidator : AbstractValidator<UpdateAvailabilityCommand>
    {
        public UpdateAvailabilityValidator()
        {
            // When updating, ensure that ClinicId is provided.
            RuleFor(x => x.ClinicId)
                .NotEmpty().WithMessage("ClinicId is required.");

            // Validate DayOfWeek is within a valid range (0 to 6).
            RuleFor(x => x.DayOfWeek)
                .InclusiveBetween(0, 6).WithMessage("DayOfWeek must be between 0 and 6.");
            
            // Ensure that the StartTime is earlier than the EndTime.
            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime).WithMessage("StartTime must be earlier than EndTime.");
        }
    }
}