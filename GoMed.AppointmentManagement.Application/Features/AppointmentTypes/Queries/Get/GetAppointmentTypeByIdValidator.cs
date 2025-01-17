using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.Get.GetAppointmentTypeById;

public class GetAppointmentTypeByIdValidator : AbstractValidator<GetAppointmentTypeById>
{
    public GetAppointmentTypeByIdValidator()
    {
        // Ensure the Id is greater than 0
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("A valid appointment type Id is required.");
    }
}