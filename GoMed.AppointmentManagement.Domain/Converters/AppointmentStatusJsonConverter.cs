using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using GoMed.AppointmentManagement.Domain.Enums;

namespace GoMed.AppointmentManagement.Domain.Converters
{
    // This class is a custom JSON converter for the AppointmentStatus enum.
    // It controls how the AppointmentStatus enum is serialized to and deserialized from JSON.
    public class AppointmentStatusJsonConverter : JsonConverter<AppointmentStatus>
    {
        // This method handles deserialization (JSON -> AppointmentStatus enum).
        // It takes a JSON string and attempts to convert it to the corresponding enum value.
        public override AppointmentStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Get the string value from the JSON reader.
            var value = reader.GetString();

            // Try to parse the string value to match an enum value in AppointmentStatus.
            // The 'true' flag makes the parsing case-insensitive (e.g., "pending" and "Pending" both work).
            return Enum.TryParse(value, true, out AppointmentStatus result)
                ? result  // If parsing succeeds, return the corresponding enum value.
                : throw new JsonException($"Invalid value for AppointmentStatus: {value}");
            // If parsing fails, throw a JsonException with a descriptive error message.
        }

        // This method handles serialization (AppointmentStatus enum -> JSON).
        // It converts the AppointmentStatus enum into a string when writing to JSON.
        public override void Write(Utf8JsonWriter writer, AppointmentStatus value, JsonSerializerOptions options)
        {
            // Write the string representation of the enum to the JSON output.
            writer.WriteStringValue(value.ToString());
        }
    }
}