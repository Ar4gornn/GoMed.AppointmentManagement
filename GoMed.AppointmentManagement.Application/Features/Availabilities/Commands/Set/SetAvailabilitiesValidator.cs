using System.Linq;
using FluentValidation;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Set.SetAvailabilities
{
    public class SetAvailabilitiesValidator : AbstractValidator<SetAvailabilities>
    {
        public SetAvailabilitiesValidator()
        {
            RuleFor(x => x.ClinicId)
                .NotEmpty().WithMessage("ClinicId is required.");

            // Validate each availability in the list
            RuleForEach(x => x.Availabilities).ChildRules(avail =>
            {
                avail.RuleFor(a => a.DayOfWeek)
                    .InclusiveBetween(0, 6)
                    .WithMessage("DayOfWeek must be between 0 (Sunday) and 6 (Saturday).");

                avail.RuleFor(a => a.StartTime)
                    .LessThan(a => a.EndTime)
                    .WithMessage("StartTime must be before EndTime.");

                avail.RuleFor(a => a.EndTime)
                    .GreaterThan(a => a.StartTime)
                    .WithMessage("EndTime must be after StartTime.");
            });

            // Validate no overlapping intervals within each DayOfWeek
            RuleFor(x => x).Custom((command, context) =>
            {
                // Group the availabilities by DayOfWeek
                var groupedByDay = command.Availabilities
                    .GroupBy(a => a.DayOfWeek);

                foreach (var group in groupedByDay)
                {
                    // Sort them by StartTime
                    var sorted = group.OrderBy(a => a.StartTime).ToList();

                    for (int i = 0; i < sorted.Count - 1; i++)
                    {
                        var current = sorted[i];
                        var next = sorted[i + 1];

                        // If current.EndTime > next.StartTime, then there is an overlap.
                        if (current.EndTime > next.StartTime)
                        {
                            context.AddFailure(
                                $"Overlapping intervals for DayOfWeek = {current.DayOfWeek}: " +
                                $"[{current.StartTime} - {current.EndTime}] overlaps " +
                                $"[{next.StartTime} - {next.EndTime}]"
                            );
                        }
                    }
                }
            });
        }
    }
}
