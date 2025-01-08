using GoMed.AppointmentManagement.Application.Common.Models;
using MediatR;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Delete.DeleteAppointmentType;

public class DeleteAppointmentType : IRequest<Result<int>>
{
    public int Id { get; init; }
    public Guid ClinicId { get; init; } 
}

