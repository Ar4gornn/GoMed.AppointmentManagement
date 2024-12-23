using System.Text.Json.Serialization;

namespace GoMed.AppointmentManagement.Domain.Enums;

/// <summary>
/// JsonConverter is used to convert the enum to string automatically when converting to JSON
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WeatherStatus
{
    Severe = 1,
    Mild = 2,
    Normal = 3
}