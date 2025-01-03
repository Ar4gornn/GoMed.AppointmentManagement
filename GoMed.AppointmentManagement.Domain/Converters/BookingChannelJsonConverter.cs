using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using GoMed.AppointmentManagement.Domain.Enums;

namespace GoMed.AppointmentManagement.Domain.Converters
{
    public class BookingChannelJsonConverter : JsonConverter<BookingChannel>
    {
        public override BookingChannel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return Enum.TryParse(value, true, out BookingChannel result) 
                ? result 
                : throw new JsonException($"Invalid value for BookingChannel: {value}");
        }

        public override void Write(Utf8JsonWriter writer, BookingChannel value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}