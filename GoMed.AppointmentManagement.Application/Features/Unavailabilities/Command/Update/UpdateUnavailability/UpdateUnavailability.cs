using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Update.UpdateUnavailability
{
    public class UpdateUnavailability : IRequest<Result>
    {
        public int Id { get; set; }
        public Guid ClinicId { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public bool IsAllDay { get; set; }
    }
}