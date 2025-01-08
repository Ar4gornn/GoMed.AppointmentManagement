using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Delete.DeleteAvailability;

public class DeleteAvailability : IRequest<Result>
{
    public int AvailabilityId { get; init; }
}