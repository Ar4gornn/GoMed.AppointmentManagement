using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Get.GetAvailabilitiesByClinic
{
    public class GetAvailabilitiesByClinicValidator : AbstractValidator<Availabilities.Get.GetAvailabilitiesByClinic.GetAvailabilitiesByClinic>
    {
        public GetAvailabilitiesByClinicValidator()
        {
            RuleFor(x => x.ClinicId)
                .NotEmpty().WithMessage("ClinicId is required.");
        }
    }
}