using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Persistence.Seed
{
    public static class UnavailabilitySeed
    {
        public static List<Unavailability> GetUnavailabilities()
        {
            // Define Clinic GUIDs (must match those in ClinicSeed.cs)
            var downtownClinicId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var uptownClinicId = Guid.Parse("44444444-4444-4444-4444-444444444444");

            return new List<Unavailability>
            {
                // Unavailability for Downtown Medical Clinic: New Year's Day (All Day)
                new Unavailability
                {
                    Id = 1,
                    ClinicId = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), // Matches Downtown Health Center
                    StartTime = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 1, 1, 23, 59, 59, TimeSpan.Zero),
                    IsAllDay = true
                },

                // Unavailability for Downtown Medical Clinic: Equipment Maintenance (Specific Time)
                new Unavailability
                {
                    Id = 2,
                    ClinicId = downtownClinicId,
                    StartTime = new DateTimeOffset(2025, 2, 15, 13, 0, 0, TimeSpan.Zero), // Feb 15, 1:00 PM
                    EndTime = new DateTimeOffset(2025, 2, 15, 17, 0, 0, TimeSpan.Zero),   // Feb 15, 5:00 PM
                    IsAllDay = false
                },

                // Unavailability for Uptown Health Center: Annual General Meeting (All Day)
                new Unavailability
                {
                    Id = 3,
                    ClinicId = uptownClinicId,
                    StartTime = new DateTimeOffset(2025, 3, 20, 0, 0, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 3, 20, 23, 59, 59, TimeSpan.Zero),
                    IsAllDay = true
                },

                // Unavailability for Uptown Health Center: Staff Training (Specific Time)
                new Unavailability
                {
                    Id = 4,
                    ClinicId = uptownClinicId,
                    StartTime = new DateTimeOffset(2025, 4, 10, 9, 0, 0, TimeSpan.Zero),  // Apr 10, 9:00 AM
                    EndTime = new DateTimeOffset(2025, 4, 10, 12, 0, 0, TimeSpan.Zero),   // Apr 10, 12:00 PM
                    IsAllDay = false
                },

                // Unavailability for Downtown Medical Clinic: Public Holiday (All Day)
                new Unavailability
                {
                    Id = 5,
                    ClinicId = downtownClinicId,
                    StartTime = new DateTimeOffset(2025, 5, 1, 0, 0, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(2025, 5, 1, 23, 59, 59, TimeSpan.Zero),
                    IsAllDay = true
                },

                // Unavailability for Uptown Health Center: System Upgrade (Specific Time)
                new Unavailability
                {
                    Id = 6,
                    ClinicId = uptownClinicId,
                    StartTime = new DateTimeOffset(2025, 6, 25, 22, 0, 0, TimeSpan.Zero), // Jun 25, 10:00 PM
                    EndTime = new DateTimeOffset(2025, 6, 26, 2, 0, 0, TimeSpan.Zero),    // Jun 26, 2:00 AM
                    IsAllDay = false
                }

                // Add more unavailabilities as needed
            };
        }
    }
}
