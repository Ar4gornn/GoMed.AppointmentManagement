using FluentValidation;
using GoMed.AppointmentManagement.Contracts.Interfaces;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Update.UpdateAppointmentType;

public class UpdateAppointmentTypeValidator : AbstractValidator<UpdateAppointmentType>
{
    public UpdateAppointmentTypeValidator(IApplicationDbContext dbContext)
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid appointment type Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0).WithMessage("Duration must be greater than zero.");

        // Additional checks for color, etc.
        RuleFor(x => x.Color)
            .MaximumLength(50).WithMessage("Color must not exceed 50 characters.");
    }
}
