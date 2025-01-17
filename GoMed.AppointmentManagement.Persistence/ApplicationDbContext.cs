using System.Reflection;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<AppointmentType> AppointmentTypes { get; set; }
    public DbSet<Availability> Availabilities { get; set; }
    public DbSet<Unavailability> Unavailabilities { get; set; }

/*    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Apply configurations for other entities
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Seed other entities here using HasData
        var appointments = AppointmentSeed.GetAppointments();
        modelBuilder.Entity<Appointment>().HasData(appointments);

        var clinics = ClinicSeed.GetClinics();
        modelBuilder.Entity<Clinic>().HasData(clinics);

        var appointmentTypes = AppointmentTypeSeed.GetAppointmentTypes();
        modelBuilder.Entity<AppointmentType>().HasData(appointmentTypes);

        // Seed Availability with Ids
        var availabilities = AvailabilitySeed.GetAvailabilities();
        modelBuilder.Entity<Availability>().HasData(availabilities);
    }*/

    public async Task SeedAvailabilityData()
    {
        if (!Availabilities.Any())
        {
            var availabilities = AvailabilitySeed.GetAvailabilities();
            await AddRangeAsync(availabilities);
            await SaveChangesAsync();
        }
    }
}
