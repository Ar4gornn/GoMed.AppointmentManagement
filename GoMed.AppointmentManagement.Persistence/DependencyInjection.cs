using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GoMed.AppointmentManagement.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("MainDbConnection") ??
                               throw new ArgumentNullException(nameof(builder.Configuration),
                                   "MainDbConnection not found");
        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });
        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }
}