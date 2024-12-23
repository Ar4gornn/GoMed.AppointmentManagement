using AppointmentManagement.Domain.Common;

namespace AppointmentManagement.Domain.Entities
{
    public class Clinic : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}