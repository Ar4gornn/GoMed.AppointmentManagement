using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Set.SetAvailabilities
{
    public class SetAvailabilities : IRequest<Result>
    {
        public Guid ClinicId { get; init; }
        
        // A list of availabilities to set for this clinic:
        public List<SetAvailabilityDto> Availabilities { get; init; } = new();
    }

    public class SetAvailabilityDto
    {
        public int DayOfWeek { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}