using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Create.CreateAvailability;

public class CreateAvailability : IRequest<Result<int>>
{
    public Guid ClinicId { get; init; }
    public int DayOfWeek { get; init; }
    public DateTimeOffset StartTime { get; init; }
    public DateTimeOffset EndTime { get; init; }
}
