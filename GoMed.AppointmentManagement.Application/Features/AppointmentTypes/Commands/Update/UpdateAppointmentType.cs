using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Update.UpdateAppointmentType;

public class UpdateAppointmentType : IRequest<Result<int>>
{
    public int Id { get; init; }
    public Guid? ClinicId { get; init; }
    public string? Name { get; init; }
    public int DurationInMinutes { get; init; }
    public string? Color { get; init; }
    public bool AllowForPatientBooking { get; init; }
}

