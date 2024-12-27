using System.ComponentModel;

namespace GoMed.AppointmentManagement.Domain.Enums
{
    public enum BookingChannel
    {
        [Description("Booked by Patient")]
        PatientBooking,

        [Description("Booked by Secretary")]
        SecretaryBooking,

        [Description("Booked by Professional")]
        ProfessionalBooking
    }
}