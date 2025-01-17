using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Persistence.Seed
{
    public static class AppointmentTypeSeed
    {
        public static List<AppointmentType> GetAppointmentTypes()
        {
            return new List<AppointmentType>
            {
                new AppointmentType
                {
                    Id = 1,
                    ClinicId = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), // Matches Downtown Health Center
                    Name = "General Consultation",
                    DurationInMinutes = 30,
                    Color = "#1E90FF",
                    AllowForPatientBooking = true
                },
                new AppointmentType
                {
                    Id = 2,
                    ClinicId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Dental Cleaning",
                    DurationInMinutes = 45,
                    Color = "#32CD32", // LimeGreen
                    AllowForPatientBooking = false
                },
                new AppointmentType
                {
                    Id = 3,
                    ClinicId = null, // Global appointment type
                    Name = "Physiotherapy Session",
                    DurationInMinutes = 60,
                    Color = "#FFD700", // Gold
                    AllowForPatientBooking = true
                },
                new AppointmentType
                {
                    Id = 4,
                    ClinicId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Eye Examination",
                    DurationInMinutes = 30,
                    Color = "#FF69B4", // HotPink
                    AllowForPatientBooking = true
                },
                new AppointmentType
                {
                    Id = 5,
                    ClinicId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Cardiology Check-up",
                    DurationInMinutes = 60,
                    Color = "#8A2BE2", // BlueViolet
                    AllowForPatientBooking = false
                },
                new AppointmentType
                {
                    Id = 6,
                    ClinicId = null,
                    Name = "Nutrition Consultation",
                    DurationInMinutes = 30,
                    Color = "#FF4500", // OrangeRed
                    AllowForPatientBooking = true
                }
                // Add more appointment types as needed
            };
        }
    }
}
