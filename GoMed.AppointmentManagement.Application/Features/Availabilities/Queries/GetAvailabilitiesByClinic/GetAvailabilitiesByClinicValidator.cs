using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Queries.GetAvailabilitiesByClinic;

public class GetAvailabilitiesByClinicValidator : AbstractValidator<GetAvailabilitiesByClinic>
{
    public GetAvailabilitiesByClinicValidator()
    {
        RuleFor(x => x.ClinicId)
            .NotEmpty().WithMessage("ClinicId is required.");
    }
}