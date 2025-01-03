using System.Text.Json.Serialization;

namespace GoMed.AppointmentManagement.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BookingChannel
    {
        Online = 0,
        Phone = 1,
        InPerson = 2
    }
}