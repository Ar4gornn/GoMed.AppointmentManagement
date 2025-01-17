using FluentValidation;
using GoMed.AppointmentManagement.Contracts.Interfaces;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Delete.DeleteAppointmentType;

public class DeleteAppointmentTypeValidator : AbstractValidator<DeleteAppointmentType>
{
    public DeleteAppointmentTypeValidator(IApplicationDbContext dbContext)
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid appointment type Id is required for deletion.");
        RuleFor(x => x.ClinicId)
            .NotEmpty().WithMessage("ClinicId is required.");
    }
}
