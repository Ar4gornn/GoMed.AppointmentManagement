using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.Get.GetAppointmentTypeById;
public class GetAppointmentTypeById : IRequest<Result<ReadAppointmentTypeDto>>
{
    public int Id { get; init; }
    public Guid ClinicId { get; init; }
}
