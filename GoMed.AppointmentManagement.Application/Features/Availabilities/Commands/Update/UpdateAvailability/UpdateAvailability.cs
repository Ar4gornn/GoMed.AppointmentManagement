using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Update.UpdateAvailability
{
    /// <summary>
    /// Command to update an existing Availability.
    /// </summary>
    public class UpdateAvailabilityCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public int DayOfWeek { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public int? ClinicId { get; set; }
    }
}