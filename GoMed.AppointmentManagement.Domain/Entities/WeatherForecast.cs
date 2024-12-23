using GoMed.AppointmentManagement.Domain.Common;
using GoMed.AppointmentManagement.Domain.Enums;

namespace GoMed.AppointmentManagement.Domain.Entities;

public class WeatherForecast : AuditableEntityBase
{
    public string? Name { get; set; }
    public WeatherStatus Status { get; set; }
    public bool IsDeleted { get; set; }
}