using System.Text.Json.Serialization;

namespace GoMed.AppointmentManagement.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BookingChannel
    {
        ProfessionalBooking = 0,
        SecretaryBooking = 1,
        PatientBooking = 2
    }
}