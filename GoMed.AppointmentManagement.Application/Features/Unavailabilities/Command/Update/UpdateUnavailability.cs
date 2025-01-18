using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Update
{
    public class UpdateUnavailability : IRequest<Result>
    {
        public int Id { get; set; }
        public Guid ClinicId { get; set; }
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset EndAt { get; set; }
        public bool IsAllDay { get; set; }
    }
}