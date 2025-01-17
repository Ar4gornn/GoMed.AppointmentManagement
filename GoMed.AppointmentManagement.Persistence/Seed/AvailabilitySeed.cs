using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Persistence.Seed;

public static class AvailabilitySeed
{
    public static List<object> GetAvailabilities()
    {
        return new List<object>
        {
            new
            {
                Id = 1,
                DayOfWeek = 1,
                StartTime = new DateTimeOffset(2025, 1, 1, 9, 0, 0, TimeSpan.Zero),
                EndTime = new DateTimeOffset(2025, 1, 1, 17, 0, 0, TimeSpan.Zero),
                ClinicId = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d")
            },
            new
            {
                Id = 2,
                DayOfWeek = 2,
                StartTime = new DateTimeOffset(2025, 1, 1, 9, 0, 0, TimeSpan.Zero),
                EndTime = new DateTimeOffset(2025, 1, 1, 17, 0, 0, TimeSpan.Zero),
                ClinicId = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d")
            },
            new
            {
                Id = 6,
                DayOfWeek = 1,
                StartTime = new DateTimeOffset(2025, 1, 1, 8, 30, 0, TimeSpan.Zero),
                EndTime = new DateTimeOffset(2025, 1, 1, 16, 30, 0, TimeSpan.Zero),
                ClinicId = Guid.Parse("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e")
            },
            new
            {
                Id = 7,
                DayOfWeek = 2,
                StartTime = new DateTimeOffset(2025, 1, 1, 8, 30, 0, TimeSpan.Zero),
                EndTime = new DateTimeOffset(2025, 1, 1, 16, 30, 0, TimeSpan.Zero),
                ClinicId = Guid.Parse("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e")
            }
        };
    }
}
