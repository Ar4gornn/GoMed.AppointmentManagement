using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Create.CreateAppointmentType;

public class CreateAppointmentType : IRequest<Result<int>>
{
    public Guid ClinicId { get; init; } // ClinicId is now required
    public string Name { get; init; }
    public int DurationInMinutes { get; init; }
    public string Color { get; init; }
    public bool AllowForPatientBooking { get; init; }
}