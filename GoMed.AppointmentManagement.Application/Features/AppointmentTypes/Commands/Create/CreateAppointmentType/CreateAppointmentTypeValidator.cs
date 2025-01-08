using FluentValidation;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Create.CreateAppointmentType;

public class CreateAppointmentTypeValidator : AbstractValidator<CreateAppointmentType>
{
    public CreateAppointmentTypeValidator(IApplicationDbContext dbContext)
    {
        // Name validation (required and unique)
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
            .MustAsync(async (request, name, cancellation) =>
            {
                // Ensure appointment type name is unique within the same clinic
                return !await dbContext.AppointmentTypes
                    .AnyAsync(a => a.ClinicId == request.ClinicId && a.Name == name, cancellation);
            }).WithMessage("An appointment type with this name already exists in the clinic.");

        // Duration validation (greater than zero)
        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0).WithMessage("Duration must be greater than zero.")
            .LessThanOrEqualTo(480).WithMessage("Duration must not exceed 8 hours (480 minutes).");

        // Color validation (max length and hex format)
        RuleFor(x => x.Color)
            .MaximumLength(7).WithMessage("Color must not exceed 7 characters.")
            .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$")
            .WithMessage("Color must be a valid hex code (e.g., #FFFFFF or #FFF).")
            .When(x => !string.IsNullOrEmpty(x.Color));
    }
}