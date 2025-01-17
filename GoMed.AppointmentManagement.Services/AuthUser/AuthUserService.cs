using System.Text.Json;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace GoMed.AppointmentManagement.Services.AuthUser;
//Pro
//add a list of ids clinicIds. match the requested clinicID to the list of clinicIds to be sure it's the right professional
//usertype == professional and clinicId of the list == clinicId of the request || admin => return true
//add field named userRoles to the AuthUserService that distinguishes between the different roles like enterprise, professional, secretary 
//add everything to interfaces 


//Patient
//add a list of ids patientIds. match the requested patientId to the list of patientIds to be sure it's the right patient
//match the profileId to the patientId and the requested appointmentId

public class AuthUserService : IAuthUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostEnvironment _hostEnvironment;

        public Guid UserId { get; init; }
        public string UserName { get; init; }
        public string UserType { get; init; }
        public bool IsDevelopment { get; init; }

        // New properties
        public IReadOnlyList<Guid> ClinicIds { get; init; }
        public IReadOnlyList<Guid> PatientIds { get; init; }
        public IReadOnlyList<string> UserRoles { get; init; }

        public AuthUserService(IHttpContextAccessor httpContextAccessor, IHostEnvironment hostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));

            if (_hostEnvironment.IsDevelopment())
            {
                // Set up some defaults for dev environment:
                UserId = Guid.Empty;
                UserName = "DeveloperUser";
                UserType = "Developer";
                IsDevelopment = true;

                // You can mock up roles or IDs in dev:
                ClinicIds = new List<Guid> 
                { 
                    // For demonstration only
                    Guid.Parse("8aa35945-8b0d-4d67-9c4b-aaf728b16000")
                };
                PatientIds = new List<Guid> 
                { 
                    // For demonstration only
                    Guid.Parse("9bc35945-8b0d-4d67-9c4b-aaf728b16111")
                };
                UserRoles = new List<string> { "admin", "developer" };
            }
            else
            {
                // Production/Non-Development
                IsDevelopment = false;
                
                var httpContext = _httpContextAccessor.HttpContext
                    ?? throw new InvalidOperationException("HttpContext is not available.");

                // Parse UserId
                UserId = httpContext.Request.Headers.TryGetValue("X-User-Id", out var id)
                    ? (Guid.TryParse(id, out var userId)
                        ? userId
                        : throw new FormatException("Invalid User Id format"))
                    : throw new ArgumentException("User Id not found in request headers");

                // Parse UserType
                UserType = httpContext.Request.Headers.TryGetValue("X-User-Type", out var type)
                    ? type.ToString()
                    : throw new ArgumentException("User Type not found in request headers");

                // Parse UserName
                UserName = httpContext.Request.Headers.TryGetValue("X-User-Name", out var name)
                    ? name.ToString()
                    : throw new ArgumentException("User Name not found in request headers");

                switch (UserType)
                {
                    case "admin":
                        return;
                    case "professional":
                    {
                        if (httpContext.Request.Headers.TryGetValue("X-User-Roles", out var rolesHeader))
                        {
                            var roles = JsonSerializer.Deserialize<List<string>>(rolesHeader);
                            UserRoles = roles ?? [];
                        }
                        else UserRoles = [];
                        // Example of parsing “X-Clinic-Ids” (comma-separated GUIDs)
                        if (httpContext.Request.Headers.TryGetValue("X-Clinic-Ids", out var clinicsHeader))
                        {
                            var clinics = JsonSerializer.Deserialize<List<Guid>>(clinicsHeader);
                            ClinicIds = clinics ?? [];
                        }
                        else
                        {
                            ClinicIds = [];
                        }

                        break;
                    }
                    case "patient":
                    {
                        if (httpContext.Request.Headers.TryGetValue("X-Patient-Ids", out var patientsHeader))
                        {
                            var patients = JsonSerializer.Deserialize<List<Guid>>(patientsHeader);
                            PatientIds = patients ?? throw new ArgumentException(nameof(patientsHeader),
                                "Patient user invalid headers");
                            ;
                        }
                        else throw new ArgumentException(nameof(patientsHeader), "Patient user invalid headers");
                    }
                        ;
                        break;
                }

                
            }
        }

        public bool IsEqualToUserId(Guid userId)
        {
            if (IsDevelopment) return true;

            return UserId == userId;
        }

        public bool CanAccessClinic(Guid clinicId)
        {
            if (IsDevelopment || UserType == "admin") return true;

            return UserType == "professional" && ClinicIds.Contains(clinicId);
        }

        public bool CanAccessPatient(Guid patientId)
        {
            if (IsDevelopment || UserType == "admin") return true;

            return UserType == "professional" && PatientIds.Contains(patientId);
        }
    }