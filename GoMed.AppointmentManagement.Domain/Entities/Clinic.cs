using System.Collections.Generic;

namespace AppointmentManagement.Domain.Entities
{
    public class Clinic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Availability> Availabilities { get; set; }
        public ICollection<Unavailability> Unavailabilities { get; set; }
    }
}