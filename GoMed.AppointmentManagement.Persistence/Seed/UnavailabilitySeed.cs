using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Persistence.Seed
{
    public static class UnavailabilitySeed
    {
        public static List<Unavailability> GetUnavailabilities()
        {
            // Fix these to match the IDs in ClinicSeed
            var downtownClinicId = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"); 
            var uptownClinicId   = Guid.Parse("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e");

            return new List<Unavailability>
            {
                // Unavailability for Downtown Health Center: New Year's Day (All Day)
                new Unavailability
                {
                    Id = 1,
                    ClinicId = downtownClinicId,
                    StartAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    EndAt = new DateTimeOffset(2025, 1, 1, 23, 59, 59, TimeSpan.Zero),
                    IsAllDay = true
                },

                // Unavailability for Downtown Health Center: Equipment Maintenance (Specific Time)
                new Unavailability
                {
                    Id = 2,
                    ClinicId = downtownClinicId,
                    StartAt = new DateTimeOffset(2025, 2, 15, 13, 0, 0, TimeSpan.Zero),
                    EndAt = new DateTimeOffset(2025, 2, 15, 17, 0, 0, TimeSpan.Zero),
                    IsAllDay = false
                },

                // Unavailability for Uptown Cardiology: Annual General Meeting (All Day)
                new Unavailability
                {
                    Id = 3,
                    ClinicId = uptownClinicId,
                    StartAt = new DateTimeOffset(2025, 3, 20, 0, 0, 0, TimeSpan.Zero),
                    EndAt = new DateTimeOffset(2025, 3, 20, 23, 59, 59, TimeSpan.Zero),
                    IsAllDay = true
                },

                // Unavailability for Uptown Cardiology: Staff Training (Specific Time)
                new Unavailability
                {
                    Id = 4,
                    ClinicId = uptownClinicId,
                    StartAt = new DateTimeOffset(2025, 4, 10, 9, 0, 0, TimeSpan.Zero),
                    EndAt = new DateTimeOffset(2025, 4, 10, 12, 0, 0, TimeSpan.Zero),
                    IsAllDay = false
                },

                // Unavailability for Downtown Health Center: Public Holiday (All Day)
                new Unavailability
                {
                    Id = 5,
                    ClinicId = downtownClinicId,
                    StartAt = new DateTimeOffset(2025, 5, 1, 0, 0, 0, TimeSpan.Zero),
                    EndAt = new DateTimeOffset(2025, 5, 1, 23, 59, 59, TimeSpan.Zero),
                    IsAllDay = true
                },

                // Unavailability for Uptown Cardiology: System Upgrade (Specific Time)
                new Unavailability
                {
                    Id = 6,
                    ClinicId = uptownClinicId,
                    StartAt = new DateTimeOffset(2025, 6, 25, 22, 0, 0, TimeSpan.Zero),
                    EndAt = new DateTimeOffset(2025, 6, 26, 2, 0, 0, TimeSpan.Zero),
                    IsAllDay = false
                }
            };
        }
    }
}
