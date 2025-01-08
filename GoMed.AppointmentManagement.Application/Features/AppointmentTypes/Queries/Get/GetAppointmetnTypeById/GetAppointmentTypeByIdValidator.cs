using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.Get.GetAppointmetnTypeById;

public class GetAppointmentTypeByIdValidator : AbstractValidator<GetAppointmentTypeById>
{
    public GetAppointmentTypeByIdValidator()
    {
        // Ensure the Id is greater than 0
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("A valid appointment type Id is required.");

        // Add a maximum value if necessary to avoid overly large Ids
        RuleFor(x => x.Id)
            .LessThanOrEqualTo(int.MaxValue).WithMessage("Appointment type Id is out of range.");

        // Ensure Id is not null (for nullable int scenarios)
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Appointment type Id must not be null.");
    }
}