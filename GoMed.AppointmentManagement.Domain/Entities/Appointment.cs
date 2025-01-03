using AppointmentManagement.Domain.Common;
using GoMed.AppointmentManagement.Domain.Enums;


namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class Appointment : AuditableEntityBase
    {
        public Guid ProfessionalId { get; set; }
        public Guid ClinicId { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Type { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public string Notes { get; set; }
        public bool ShowedUp { get; set; }
        public BookingChannel BookingChannel { get; set; } = BookingChannel.Online;
        public Clinic Clinic { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}