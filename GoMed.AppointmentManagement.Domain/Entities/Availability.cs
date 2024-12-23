using System;
using AppointmentManagement.Domain.Common;

namespace AppointmentManagement.Domain.Entities
{
    public class Availability : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid ClinicId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}