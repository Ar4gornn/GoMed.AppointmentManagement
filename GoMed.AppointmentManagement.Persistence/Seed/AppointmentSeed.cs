using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Enums;

namespace GoMed.AppointmentManagement.Persistence.Seed
{
    public static class AppointmentSeed
    {
        public static List<Appointment> GetAppointments()
        {
            return new List<Appointment>
            {
                new Appointment
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    ProfessionalId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 
                    ClinicId = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), 
                    PatientId = Guid.Parse("44444444-4444-4444-4444-444444444444"),  
                    PatientName = "John Doe",
                    PatientPhone = "+1234567890",
                    StartAt = DateTimeOffset.UtcNow.AddDays(1).AddHours(9),
                    EndAt = DateTimeOffset.UtcNow.AddDays(1).AddHours(10),
                    Type = "General Consultation",
                    Status = AppointmentStatus.Confirmed,
                    Notes = "First-time consultation.",
                    ShowedUp = true,
                    BookingChannel = BookingChannel.PatientBooking,
                    Created = DateTimeOffset.UtcNow,
                    CreatedBy = "Seeder",
                    LastModified = DateTimeOffset.UtcNow,
                    LastModifiedBy = "Seeder"
                },
                new Appointment
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    ProfessionalId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    // Originally "33333333-3333-3333-3333-333333333333"
                    // Let’s say we link this to Uptown Cardiology:
                    ClinicId = Guid.Parse("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e"), 
                    PatientId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    PatientName = "Jane Smith",
                    PatientPhone = "+0987654321",
                    StartAt = DateTimeOffset.UtcNow.AddDays(2).AddHours(14),
                    EndAt = DateTimeOffset.UtcNow.AddDays(2).AddHours(15),
                    Type = "Dental Cleaning",
                    Status = AppointmentStatus.Pending,
                    Notes = "Patient requests morning slot.",
                    ShowedUp = false,
                    BookingChannel = BookingChannel.SecretaryBooking,
                    Created = DateTimeOffset.UtcNow,
                    CreatedBy = "Seeder",
                    LastModified = DateTimeOffset.UtcNow,
                    LastModifiedBy = "Seeder"
                },
                new Appointment
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    ProfessionalId = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    // Originally "33333333-3333-3333-3333-333333333333"
                    // Suppose we link this one to Greenwood Pediatrics:
                    ClinicId = Guid.Parse("3c4d5e6f-7a8b-9c0d-1e2f-3a4b5c6d7e8f"), 
                    PatientId = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    PatientName = "Alice Johnson",
                    PatientPhone = "+1122334455",
                    StartAt = DateTimeOffset.UtcNow.AddDays(-1).AddHours(11),
                    EndAt = DateTimeOffset.UtcNow.AddDays(-1).AddHours(12),
                    Type = "Physiotherapy Session",
                    Status = AppointmentStatus.Completed,
                    Notes = "Follow-up session.",
                    ShowedUp = true,
                    BookingChannel = BookingChannel.ProfessionalBooking,
                    Created = DateTimeOffset.UtcNow.AddDays(-2),
                    CreatedBy = "Seeder",
                    LastModified = DateTimeOffset.UtcNow.AddDays(-1),
                    LastModifiedBy = "Seeder"
                },
                new Appointment
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    ProfessionalId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    // Originally "33333333-3333-3333-3333-333333333333"
                    // Let’s link this also to Uptown Cardiology:
                    ClinicId = Guid.Parse("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e"),
                    PatientId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    PatientName = "Bob Williams",
                    PatientPhone = "+5566778899",
                    StartAt = DateTimeOffset.UtcNow.AddDays(3).AddHours(16),
                    EndAt = DateTimeOffset.UtcNow.AddDays(3).AddHours(17),
                    Type = "Eye Examination",
                    Status = AppointmentStatus.Cancelled,
                    Notes = "Patient cancelled due to personal reasons.",
                    ShowedUp = false,
                    BookingChannel = BookingChannel.PatientBooking,
                    Created = DateTimeOffset.UtcNow,
                    CreatedBy = "Seeder",
                    LastModified = DateTimeOffset.UtcNow,
                    LastModifiedBy = "Seeder"
                }
            };
        }
    }
}
