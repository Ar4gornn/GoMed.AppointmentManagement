using System.Text.Json.Serialization;

namespace GoMed.AppointmentManagement.Domain.Enums
{
    /// <summary>
    /// JsonConverter is used to convert the enum to string automatically when converting to JSON
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ClinicStatus
    {
        Active = 1,       // Clinic is operational and accepting appointments
        Inactive = 2,     // Clinic is temporarily closed or not accepting appointments
        UnderMaintenance = 3,  // Clinic is undergoing maintenance
        ClosedPermanently = 4  // Clinic is permanently closed
    }
}