using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Persistence.Seed
{
    public static class ClinicSeed
    {
        public static List<Clinic> GetClinics()
        {
            return new List<Clinic>
            {
                new Clinic
                {
                    Id = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
                    ProfessionalId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Matches ProfessionalSeed
                    ProfessionalName = "Dr. Emily Stone",
                    Name = "Downtown Health Center",
                    Title = "Primary Care Clinic",
                    PictureUrl = "https://example.com/images/downtown_health_center.png",
                    Speciality = "General Medicine",
                    Address = "123 Elm Street",
                    DetailedAddress = "Suite 100, Floor 1",
                    MapUrl = "https://maps.example.com/?q=123+Elm+Street",
                    AllowNewPatientBooking = true,
                    AllowPatientBooking = true,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow,
                    IsActive = true,
                    PatientBookingIntervalInMinutes = 20
                },
                new Clinic
                {
                    Id = Guid.Parse("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e"),
                    ProfessionalId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    ProfessionalName = "Dr. Michael Lee",
                    Name = "Uptown Cardiology",
                    Title = "Cardiology Specialist",
                    PictureUrl = "https://example.com/images/uptown_cardiology.png",
                    Speciality = "Cardiology",
                    Address = "456 Oak Avenue",
                    DetailedAddress = "Building B, Floor 3",
                    MapUrl = "https://maps.example.com/?q=456+Oak+Avenue",
                    AllowNewPatientBooking = false,
                    AllowPatientBooking = true,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow,
                    IsActive = true,
                    PatientBookingIntervalInMinutes = 30
                },
                new Clinic
                {
                    Id = Guid.Parse("3c4d5e6f-7a8b-9c0d-1e2f-3a4b5c6d7e8f"),
                    ProfessionalId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    ProfessionalName = "Dr. Sophia Ramirez",
                    Name = "Greenwood Pediatrics",
                    Title = "Pediatric Clinic",
                    PictureUrl = "https://example.com/images/greenwood_pediatrics.png",
                    Speciality = "Pediatrics",
                    Address = "789 Pine Road",
                    DetailedAddress = "Floor 2, Suite 200",
                    MapUrl = "https://maps.example.com/?q=789+Pine+Road",
                    AllowNewPatientBooking = true,
                    AllowPatientBooking = false,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow,
                    IsActive = true,
                    PatientBookingIntervalInMinutes = 15
                }
                // Add more clinics as needed
            };
        }
    }
}
