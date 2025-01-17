using Microsoft.EntityFrameworkCore;
using GoMed.AppointmentManagement.Domain.Entities;

namespace GoMed.AppointmentManagement.Persistence.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Clinics
            var clinics = ClinicSeed.GetClinics();
            modelBuilder.Entity<Clinic>().HasData(clinics);
            

            // Then Availabilities
            var availabilities = AvailabilitySeed.GetAvailabilities();
            modelBuilder.Entity<Availability>().HasData(availabilities);

            // Seed Appointments
            var appointments = AppointmentSeed.GetAppointments();
            modelBuilder.Entity<Appointment>().HasData(appointments);

            // Seed AppointmentTypes
            var appointmentTypes = AppointmentTypeSeed.GetAppointmentTypes();
            modelBuilder.Entity<AppointmentType>().HasData(appointmentTypes);

            // Seed Unavailabilities
            var unavailabilities = UnavailabilitySeed.GetUnavailabilities();
            modelBuilder.Entity<Unavailability>().HasData(unavailabilities);
            
        }
    }
}
