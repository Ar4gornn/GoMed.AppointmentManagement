namespace GoMed.AppointmentManagement.Application.Features.AppointmentManagement.Dtos;

/// <summary>
/// DTO for canceling an existing appointment.
/// </summary>
public class CancelAppointmentDto
{
    /// <summary>
    /// The unique identifier of the appointment to cancel.
    /// </summary>
    public Guid AppointmentId { get; set; }

    /// <summary>
    /// Reason for cancellation (optional).
    /// </summary>
    public string CancellationReason { get; set; }

    /// <summary>
    /// Flag to indicate if clinic policies (e.g., cancellation window) should be overridden.
    /// Used by clinic admins or special cases.
    /// </summary>
    public bool OverridePolicy { get; set; }
}