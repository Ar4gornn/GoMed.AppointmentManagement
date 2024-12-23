using System.Text.Json;
using GoMed.AppointmentManagement.Application.Common.Helpers;
using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Application.Features.WeatherForecasts.Dtos;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GoMed.AppointmentManagement.Application.Features.WeatherForecasts.Queries.GetAll;

public class GetAllWeatherForecast : IRequest<Result<IEnumerable<ReadWeatherForecastDto>>>
{
    public int PageNumber { get; init; } = 1;
}

public class GetAllWeatherForecastQueryHandler(
    IApplicationDbContext dbContext,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetAllWeatherForecast, Result<IEnumerable<ReadWeatherForecastDto>>>
{
    public async Task<Result<IEnumerable<ReadWeatherForecastDto>>> Handle(GetAllWeatherForecast request,
        CancellationToken cancellationToken)
    {
        var result = await dbContext.WeatherForecasts.AsNoTracking().PaginateQuery();
        var weatherForecasts = await result.paginatedQuery.Select(wf => new ReadWeatherForecastDto
        {
            Id = wf.Id,
            Name = wf.Name,
        }).ToListAsync(cancellationToken);

        httpContextAccessor.HttpContext!.Response.Headers.Append("X-Pagination",
            JsonSerializer.Serialize(result.paginationMetadata));
        return Result<IEnumerable<ReadWeatherForecastDto>>.Success(weatherForecasts);
    }
}