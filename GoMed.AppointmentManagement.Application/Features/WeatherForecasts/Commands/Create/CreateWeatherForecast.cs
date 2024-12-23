using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using GoMed.AppointmentManagement.Domain.Entities;
using GoMed.AppointmentManagement.Domain.Enums;
using GoMed.AppointmentManagement.Domain.Events;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.WeatherForecasts.Commands.Create;

public class CreateWeatherForecast : IRequest<Result<Guid>>
{
    public string? Name { get; init; }
    public WeatherStatus? Status { get; init; }
}

public class CreateWeatherForecastCommandHandler(
    IApplicationDbContext dbContext,
    IPublishEndpoint publishEndpoint,
    IMediator mediator) : IRequestHandler<CreateWeatherForecast, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateWeatherForecast request, CancellationToken cancellationToken)
    {
        if (await dbContext.WeatherForecasts.AnyAsync(w => w.Name == request.Name, cancellationToken))
            return Result<Guid>.Conflict("WeatherForecast.NameAlreadyExists",
                "Weather forecast with the same name already exists.");
        var weatherForecast = new WeatherForecast
        {
            Name = request.Name,
            Status = request.Status ?? WeatherStatus.Normal
        };


        var addedEntry = await dbContext.WeatherForecasts.AddAsync(weatherForecast, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var newEvent = new WeatherForecastCreatedEvent(addedEntry.Entity);

        await publishEndpoint.Publish(newEvent, cancellationToken);
        await mediator.Publish(newEvent, cancellationToken);

        return Result<Guid>.Success(addedEntry.Entity.Id);
    }
}