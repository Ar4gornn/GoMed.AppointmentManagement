using AppointmentManagement.Domain.Common;

namespace AppointmentManagement.Domain.Entities
{
    public class Appointment : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid ProfessionalId { get; set; }
        public Guid ClinicId { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public bool ShowedUp { get; set; }
        public string BookingChannel { get; set; }

        public Clinic Clinic { get; set; }
        public Patient Patient { get; set; }
        public AppointmentStatus Status { get; set; }

    }
}