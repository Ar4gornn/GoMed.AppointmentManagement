using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Domain.Events;

public record AppointmentNoShowEvent(Appointment AppointmentData);