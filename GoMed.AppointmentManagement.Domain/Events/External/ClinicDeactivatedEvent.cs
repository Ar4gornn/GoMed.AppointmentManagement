using GoMed.AppointmentManagement.Domain.Entities;
using Clinic = GoMed.AppointmentManagement.Domain.Enums.Clinic;

namespace GoMed.AppointmentManagement.Domain.Events.External;

/// <summary>
/// This external event is triggered when a clinic is deactivated.
/// Other microservices can use this to manage dependencies.
/// </summary>
/// <param name="ClinicData"></param>
public record ClinicDeactivatedEvent(Clinic ClinicData);