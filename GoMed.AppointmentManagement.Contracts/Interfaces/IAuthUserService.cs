namespace GoMed.AppointmentManagement.Contracts.Interfaces
{
    public interface IAuthUserService
    {
        Guid UserId { get; }
        string UserName { get; }

        /// <summary>
        /// Gets the user type (e.g., "Patient", "Professional", "Admin", etc.).
        /// </summary>
        string UserType { get; }

        /// <summary>
        /// Indicates whether the application is running in Development mode.
        /// </summary>
        bool IsDevelopment { get; }

        /// <summary>
        /// A collection of clinic IDs associated with the user (if relevant).
        /// </summary>
        IReadOnlyList<Guid> ClinicIds { get; }

        /// <summary>
        /// A collection of patient IDs associated with the user (if relevant).
        /// </summary>
        IReadOnlyList<Guid> PatientIds { get; }

        /// <summary>
        /// A collection of roles that this user has. 
        /// e.g. "admin", "professional", "secretary", "enterprise"
        /// </summary>
        IReadOnlyList<string> UserRoles { get; }

        /// <summary>
        /// Checks whether the current user ID matches the provided userId.
        /// </summary>
        bool IsEqualToUserId(Guid userId);

        /// <summary>
        /// Checks if the current user can access the specified clinic.
        ///   - If user has "admin" role => always true
        ///   - If user has "professional" role and the requested clinicId is in ClinicIds => true
        ///   - Otherwise => false
        /// </summary>
        bool CanAccessClinic(Guid clinicId);

        /// <summary>
        /// Checks if the current user can access the specified patient.
        ///   - If user has "admin" role => always true
        ///   - If user has "patient" role and the requested patientId is in PatientIds => true
        ///   - Otherwise => false
        /// </summary>
        bool CanAccessPatient(Guid patientId);
    }
}