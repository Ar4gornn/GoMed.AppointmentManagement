using System.Net;
using System.Text.Json;
using GoMed.AppointmentManagement.Application.Features.WeatherForecasts.Commands.Create;
using GoMed.AppointmentManagement.Application.Features.WeatherForecasts.Dtos;
using GoMed.AppointmentManagement.Application.Features.WeatherForecasts.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GoMed.AppointmentManagement.WebApi.Endpoints;

public static class WeatherForecastEndpoints
{
    public static void AddWeatherForecastEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/weather-forecast")
            .WithTags("WeatherForecast")
            .WithOpenApi();

        group.MapGet("/", GetAll)
            .Produces<IEnumerable<ReadWeatherForecastDto>>(StatusCodes.Status200OK);
        group.MapPost("/", Create)
            .Produces<Guid>(StatusCodes.Status201Created);
    }

    private static async Task<IResult> GetAll(HttpContext context, IMediator mediator,
        [AsParameters] int pageNumber = 1)
    {
        var response = await mediator.Send(new GetAllWeatherForecast());

        return response.ToIResult();
    }

    private static async Task<IResult> Create(HttpContext context, IMediator mediator, CreateWeatherForecast request)
    {
        var response = await mediator.Send(request);
        return response.ToIResult();
    }
}