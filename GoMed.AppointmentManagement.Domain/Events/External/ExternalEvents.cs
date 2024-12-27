namespace GoMed.AppointmentManagement.Domain.Events.External
{
    /// <summary>
    /// External Events are events that are sent from other microservices
    /// </summary>
    public class ClinicExternalEvent
    {
        public Guid ClinicId { get; set; }
        public string EventType { get; set; }
        public DateTime EventTime { get; set; }
    }

    public class AppointmentExternalEvent
    {
        public Guid AppointmentId { get; set; }
        public string EventType { get; set; }
        public DateTime EventTime { get; set; }
    }
}