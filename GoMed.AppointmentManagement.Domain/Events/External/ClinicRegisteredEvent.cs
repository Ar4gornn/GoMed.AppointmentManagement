using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events.External;

/// <summary>
/// This external event is triggered when a new clinic is registered in the system.
/// It is sent to other microservices to synchronize data.
/// </summary>
/// <param name="ClinicData"></param>
public record ClinicRegisteredEvent(Clinic ClinicData);