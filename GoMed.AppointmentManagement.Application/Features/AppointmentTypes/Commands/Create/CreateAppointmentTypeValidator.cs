using FluentValidation;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using MassTransit.Futures.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Create.CreateAppointmentType;

public class CreateAppointmentTypeValidator : AbstractValidator<CreateAppointmentType>
{
    public CreateAppointmentTypeValidator()
    {
        RuleFor(x => x.ClinicId).NotEmpty().WithMessage("ClinicId is required.");
        // Name validation (required and unique)
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");
            

        // Duration validation (greater than zero)
        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0).WithMessage("Duration must be greater than zero.")
            .LessThanOrEqualTo(480).WithMessage("Duration must not exceed 8 hours (480 minutes).");

        // Color validation (max length and hex format)
        RuleFor(x => x.Color)
            .MaximumLength(7).WithMessage("Color must not exceed 7 characters.")
            .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$")
            .WithMessage("Color must be a valid hex code (e.g., #FFFFFF or #FFF).");
    }
}