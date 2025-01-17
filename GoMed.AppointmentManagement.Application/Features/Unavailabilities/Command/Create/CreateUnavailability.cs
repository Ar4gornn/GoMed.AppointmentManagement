using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Create.CreateUnavailability
{
    public class CreateUnavailability : IRequest<Result<int>>
    {
        // Directly taking the same fields from CreateUnavailabilityDto
        public Guid ClinicId { get; set; }
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset EndAt { get; set; }
        public bool IsAllDay { get; set; }
    }
}