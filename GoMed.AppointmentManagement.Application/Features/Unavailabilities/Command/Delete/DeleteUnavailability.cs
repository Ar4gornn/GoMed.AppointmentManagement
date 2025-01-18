using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Delete
{
    public class DeleteUnavailability : IRequest<Result>
    {
        public int Id { get; set; }
        public Guid ClinicId { get; set; }
    }
}