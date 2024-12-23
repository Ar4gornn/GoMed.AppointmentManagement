using System.Reflection;
using GoMed.AppointmentManagement.Application.Common.Behaviours;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GoMed.AppointmentManagement.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        #region MassTransit Config

        builder.Services.AddMassTransit(x =>
        {
            var entryAssembly = Assembly.GetExecutingAssembly();
            var connectionString = builder.Configuration.GetConnectionString("RabbitMqConnection") ??
                                   (builder.Environment.IsDevelopment()
                                       ? null
                                       : throw new ArgumentNullException(nameof(builder.Configuration),
                                           "RabbitMqConnection not found"));

            x.SetKebabCaseEndpointNameFormatter();
            x.SetInMemorySagaRepositoryProvider();

            x.AddConsumers(entryAssembly);
            x.AddSagaStateMachines(entryAssembly);
            x.AddSagas(entryAssembly);
            x.AddActivities(entryAssembly);

            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(connectionString);
                configurator.ConfigureEndpoints(context);
            });
        });

        #endregion
    }
}