using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events.External;

public record ClinicUpdatedEvent(Clinic ClinicData);