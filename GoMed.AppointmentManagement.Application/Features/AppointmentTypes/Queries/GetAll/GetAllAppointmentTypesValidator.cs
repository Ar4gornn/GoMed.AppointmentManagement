using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.GetAll;

public class GetAllAppointmentTypesValidator : AbstractValidator<GetAllAppointmentTypes>
{
    public GetAllAppointmentTypesValidator()
    {
        // ClinicId must not be empty if provided
        RuleFor(x => x.ClinicId)
            .NotEmpty().WithMessage("Clinic ID is required.");
    }
}