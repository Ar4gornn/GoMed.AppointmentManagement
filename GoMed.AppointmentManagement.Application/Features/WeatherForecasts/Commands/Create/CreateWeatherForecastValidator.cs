using GoMed.AppointmentManagement.Contracts.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.WeatherForecasts.Commands.Create;

public class CreateWeatherForecastValidator : AbstractValidator<CreateWeatherForecast>
{
    public CreateWeatherForecastValidator(IApplicationDbContext dbContext)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status is not valid.");
    }
}