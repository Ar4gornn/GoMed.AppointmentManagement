namespace GoMed.AppointmentManagement.Domain.Entities
{
    public class Unavailability
    {
        public int Id { get; set; }
        public Guid? ClinicId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public bool IsAllDay { get; set; }

        public Clinic? Clinic { get; set; }
    }
}


//create a class which makes calculations for the available slots
//add chosen slots to the list of unavailability
//8 /8:15/ 830 845

//8:15 8:30; 12 break 13