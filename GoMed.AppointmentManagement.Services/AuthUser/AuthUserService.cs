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

    public AuthUserService(IHttpContextAccessor httpContextAccessor, IHostEnvironment hostEnvironment)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));

        if (hostEnvironment.IsDevelopment())
        {
            UserId = Guid.Empty;
            UserName = "Hyroglatos";
            UserType = "Developer";
            IsDevelopment = true;
        }
        else
        {
            var httpContext = _httpContextAccessor.HttpContext
                              ?? throw new InvalidOperationException("HttpContext is not available.");

            UserId = httpContext.Request.Headers.TryGetValue("X-User-Id", out var id)
                ? Guid.TryParse(id, out var userId)
                    ? userId
                    : throw new FormatException("Invalid User Id format")
                : throw new ArgumentException("User Id not found in request headers");

            UserType = httpContext.Request.Headers.TryGetValue("X-User-Type", out var type)
                ? type.ToString()
                : throw new ArgumentException("User Type not found in request headers");

            UserName = httpContext.Request.Headers.TryGetValue("X-User-Name", out var name)
                ? name.ToString()
                : throw new ArgumentException("User Name not found in request headers");

            IsDevelopment = false;
        }
    }

    public bool IsEqualToUserId(Guid userId)
    {
        if (IsDevelopment) return true;
        return UserId == userId;
    }
}