using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Persistence.Seed
{
    public static class AvailabilitySeed
    {
        public static List<Availability> GetAvailabilities()
        {
            return new List<Availability>
            {
                // Availability for Downtown Medical Clinic
                new Availability
                {
                    DayOfWeek = 1, // Monday
                    StartTime = new DateTimeOffset(2025, 1, 1, 9, 0, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 1, 1, 17, 0, 0, TimeSpan.Zero),
                    Clinic = null // The actual link will be handled by the ORM based on foreign key configuration
                },
                new Availability
                {
                    DayOfWeek = 2, // Tuesday
                    StartTime = new DateTimeOffset(2025, 1, 1, 9, 0, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 1, 1, 17, 0, 0, TimeSpan.Zero),
                    Clinic = null // The actual link will be handled by the ORM based on foreign key configuration
                },
                new Availability
                {
                    DayOfWeek = 3, // Wednesday
                    StartTime = new DateTimeOffset(2025, 1, 1, 9, 0, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 1, 1, 17, 0, 0, TimeSpan.Zero),
                    Clinic = null
                },
                new Availability
                {
                    DayOfWeek = 4, // Thursday
                    StartTime = new DateTimeOffset(2025, 1, 1, 9, 0, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 1, 1, 17, 0, 0, TimeSpan.Zero),
                    Clinic = null
                },
                new Availability
                {
                    DayOfWeek = 5, // Friday
                    StartTime = new DateTimeOffset(2025, 1, 1, 9, 0, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 1, 1, 17, 0, 0, TimeSpan.Zero),
                    Clinic = null
                },

                // Availability for Uptown Health Center
                new Availability
                {
                    DayOfWeek = 1, // Monday
                    StartTime = new DateTimeOffset(2025, 1, 1, 8, 30, 0, TimeSpan.Zero), // 08:30 AM
                    EndTime = new DateTimeOffset(2025, 1, 1, 16, 30, 0, TimeSpan.Zero), // 04:30 PM
                    Clinic = null
                },
                new Availability
                {
                    DayOfWeek = 2, // Tuesday
                    StartTime = new DateTimeOffset(2025, 1, 1, 8, 30, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 1, 1, 16, 30, 0, TimeSpan.Zero),
                    Clinic = null
                },
                new Availability
                {
                    DayOfWeek = 3, // Wednesday
                    StartTime = new DateTimeOffset(2025, 1, 1, 8, 30, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 1, 1, 16, 30, 0, TimeSpan.Zero),
                    Clinic = null
                },
                new Availability
                {
                    DayOfWeek = 4, // Thursday
                    StartTime = new DateTimeOffset(2025, 1, 1, 8, 30, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 1, 1, 16, 30, 0, TimeSpan.Zero),
                    Clinic = null
                },
                new Availability
                {
                    DayOfWeek = 5, // Friday
                    StartTime = new DateTimeOffset(2025, 1, 1, 8, 30, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 1, 1, 16, 30, 0, TimeSpan.Zero),
                    Clinic = null
                },

                // Example: Specific availability for a particular clinic on Saturday
                new Availability
                {
                    DayOfWeek = 6, // Saturday
                    StartTime = new DateTimeOffset(2025, 1, 1, 10, 0, 0, TimeSpan.Zero), // 10:00 AM
                    EndTime = new DateTimeOffset(2025, 1, 1, 14, 0, 0, TimeSpan.Zero), // 02:00 PM
                    Clinic = null
                }

                // Add more availabilities as needed
            };
        }
    }
}
