using GoMed.AppointmentManagement.Application;
using GoMed.AppointmentManagement.Persistence;
using GoMed.AppointmentManagement.Services;
using GoMed.AppointmentManagement.WebApi.Endpoints;
using GoMed.AppointmentManagement.WebApi.Middlewares;
using Serilog;

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
    // app.SeedDatabase();
}

// Exception handling
app.UseExceptionHandler();

// Security and redirection
app.UseHttpsRedirection();

// CORS
app.UseCors("AllowAll");

// Endpoints
app.AddWeatherForecastEndpoints();

app.Run();