using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Update.UpdateAvailability;

public class UpdateAvailability : IRequest<Result>
{
    public Guid ClinicId { get; init; }
    public int DayOfWeek { get; init; }

    // For updating, we keep StartTime as part of the "composite key" to look up the record.
    public DateTimeOffset StartTime { get; init; }

    // The new EndTime to update to
    public DateTimeOffset EndTime { get; init; }
}