using AppointmentManagement.Domain.Common;

namespace AppointmentManagement.Domain.Entities
{
    public class Patient : AuditableEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}