
namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class AppointmentType
    {
        public int Id { get; private set; }
        public int ClinicId { get; private set; }
        public string Name { get; private set; }
        
    }
}