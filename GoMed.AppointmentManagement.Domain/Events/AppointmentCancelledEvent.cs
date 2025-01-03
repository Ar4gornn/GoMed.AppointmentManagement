using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;

namespace AppointmentManagement.Domain.Events;

/// <summary>
/// This event is triggered internally when an appointment is cancelled.
/// It can notify relevant services or update logs.
/// </summary>
/// <param name="AppointmentData"></param>
public record AppointmentCancelledEvent(Appointment AppointmentData);