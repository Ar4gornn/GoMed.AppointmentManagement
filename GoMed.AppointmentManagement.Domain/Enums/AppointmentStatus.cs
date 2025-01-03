using System.Text.Json.Serialization;
using GoMed.AppointmentManagement.Domain.Converters;

namespace GoMed.AppointmentManagement.Domain.Enums
{

    /// <summary>
    /// JsonConverter is used to convert the enum to string automatically when converting to JSON
    /// </summary>
    [JsonConverter(typeof(AppointmentStatusJsonConverter))]
    public enum AppointmentStatus
    {
        Pending = 0,
        Confirmed = 1,
        Cancelled = 2,
        Completed = 3
    }
}