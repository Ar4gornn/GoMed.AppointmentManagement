using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.Appointments.Queries.GetPatientAppointmentHistoryQuery
{
    public class GetPatientAppointmentHistoryQueryHandler : IRequestHandler<GetPatientAppointmentHistoryQuery, Result<List<Appointment>>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IAuthUserService _authUserService;

        public GetPatientAppointmentHistoryQueryHandler(IApplicationDbContext dbContext, IAuthUserService authUserService)
        {
            _dbContext = dbContext;
            _authUserService = authUserService;
        }

        public async Task<Result<List<Appointment>>> Handle(GetPatientAppointmentHistoryQuery request, CancellationToken cancellationToken)
        {
            // 1) Check access permissions
            if (!_authUserService.CanAccessPatient(request.PatientId))
            {
                return Result<List<Appointment>>.Unauthorized(
                    "Appointment.Unauthorized",
                    "You do not have permission to view this patient's appointment history."
                );
            }

            // 2) Retrieve appointments from the database
            var pageSize = 20; // Example page size; change as desired
            var skip = (request.Page - 1) * pageSize;

            var appointments = await _dbContext.Appointments
                .Where(a => a.PatientId == request.PatientId)
                .OrderByDescending(a => a.StartAt)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            // 3) Handle empty results scenario
            if (appointments.Count == 0)
            {
                return Result<List<Appointment>>.NotFound(
                    "AppointmentHistory.NotFound",
                    "No historical appointments found for this patient."
                );
            }

            // 4) Return a success result
            return Result<List<Appointment>>.Success(appointments);
        }
    }

}
