using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events;

public record AppointmentRescheduledEvent(Appointment AppointmentData);