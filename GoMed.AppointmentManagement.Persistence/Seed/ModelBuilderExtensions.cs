// GoMed.AppointmentManagement.Persistence/Seed/ModelBuilderExtensions.cs

using Microsoft.EntityFrameworkCore;
using GoMed.AppointmentManagement.Domain.Entities;
using System.Collections.Generic;

namespace GoMed.AppointmentManagement.Persistence.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Appointments
            var appointments = AppointmentSeed.GetAppointments();
            modelBuilder.Entity<Appointment>().HasData(appointments);
            
            // Seed Clinics
            var clinics = ClinicSeed.GetClinics();
            modelBuilder.Entity<Clinic>().HasData(clinics);
            
            // Seed AppointmentTypes
            var appointmentTypes = AppointmentTypeSeed.GetAppointmentTypes();
            modelBuilder.Entity<AppointmentType>().HasData(appointmentTypes);
            
            // Seed Availabilities
            var availabilities = AvailabilitySeed.GetAvailabilities();
            modelBuilder.Entity<Availability>().HasData(availabilities);

            // Seed Unavailabilities
            var unavailabilities = UnavailabilitySeed.GetUnavailabilities();
            modelBuilder.Entity<Unavailability>().HasData(unavailabilities);
            
            

            // If you have other seed data (e.g., Professionals, Patients), seed them here as well
            // Example:
            // modelBuilder.Entity<Professional>().HasData(ProfessionalSeed.GetProfessionals());
        }
    }
}