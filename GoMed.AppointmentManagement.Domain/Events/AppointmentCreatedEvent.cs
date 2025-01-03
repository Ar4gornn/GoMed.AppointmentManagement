using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;

namespace AppointmentManagement.Domain.Events;

/// <summary>
/// This event will be published internally when an appointment is created.
/// It can trigger workflows or notifications inside the system.
/// </summary>
/// <param name="AppointmentData"></param>
public record AppointmentCreatedEvent(Appointment AppointmentData);