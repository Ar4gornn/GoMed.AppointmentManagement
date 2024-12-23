using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Services.AuthUser;
using GoMed.AppointmentManagement.Services.FileStorage;
using GoMed.AppointmentManagement.Services.Redis;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace GoMed.AppointmentManagement.Services;

public static class DependencyInjection
{
    public static void AddServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection") ??
                                          throw new ArgumentException("RedisConnection is not configured")));
        builder.Services.AddScoped<ICachingService, CachingService>();
        builder.Services.AddAzureClients(b =>
        {
            b.AddBlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorageConnection"));
        });
        builder.Services.AddScoped<IAuthUserService, AuthUserService>();
        builder.Services.AddScoped<IImageService, AzureBlobService>();
    }
}