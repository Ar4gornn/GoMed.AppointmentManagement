// Program.cs

using GoMed.AppointmentManagement.Application;
using GoMed.AppointmentManagement.Persistence;
using GoMed.AppointmentManagement.Services;
using GoMed.AppointmentManagement.WebApi.Endpoints;
using GoMed.AppointmentManagement.WebApi.Middlewares;
using Serilog;
using Microsoft.EntityFrameworkCore; // Ensure this using directive is present
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Logging configuration
builder.Services.AddHttpContextAccessor();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder => policyBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("X-Pagination"));
});



// Swagger and API Explorer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Clean Architecture layers and services
builder.AddApplication();
builder.AddPersistence();
builder.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline
// Development-specific middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply migrations and seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Apply any pending migrations
        context.Database.Migrate();

        // Seed Availability Data
        await context.SeedAvailabilityData();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        // Optionally, rethrow the exception if you want the application to stop
        // throw;
    }
}

// Exception handling
app.UseExceptionHandler();

// Security and redirection
app.UseHttpsRedirection();

// CORS
app.UseCors("AllowAll");

// Endpoints
app.AddAppointmentEndpoints();
app.AddAvailabilityEndpoints();
app.AddUnavailabilityEndpoints();
app.AddAppointmentTypeEndpoints();

app.Run();
