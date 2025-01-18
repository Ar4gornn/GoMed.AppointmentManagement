using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Get
{
    public class GetAvailabilitiesByClinicValidator : AbstractValidator<GetAvailabilitiesByClinic>
    {
        public GetAvailabilitiesByClinicValidator()
        {
            RuleFor(x => x.ClinicId)
                .NotEmpty().WithMessage("ClinicId is required.");
        }
    }
}