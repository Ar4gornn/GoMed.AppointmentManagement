using System.Diagnostics;
using System.Text.Json;
using GoMed.AppointmentManagement.Application.Common.Models;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace GoMed.AppointmentManagement.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>(
    ILogger<LoggingBehaviour<TRequest, TResponse>> logger,
    IAuthUserService authUserService)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : class
{
    private readonly Stopwatch _timer = new();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = authUserService.UserId;
        var userType = authUserService.UserType;

        logger.LogInformation("GoMed.AppointmentManagement Request: {Name} {UserId} {UserType} {@Request}",
            requestName, userId, userType, request);

        _timer.Start();

        var result = await next();

        _timer.Stop();


        if (_timer.ElapsedMilliseconds > 500)
        {
            logger.LogWarning(
                "GoMed.AppointmentManagement request {Name} {UserId} {UserType} took {ElapsedMilliseconds}ms. {@Request}",
                requestName, userId, userType, _timer.ElapsedMilliseconds, request);
        }

        if (result is Result resultResult)
        {
            if (!resultResult.IsSuccess)
            {
                using (LogContext.PushProperty("Error", JsonSerializer.Serialize(resultResult.Error)))
                {
                    logger.LogError(
                        "GoMed.AppointmentManagement request {Name} {UserId} {UserType} failed after {ElapsedMilliseconds}ms. {@Request}",
                        requestName, userId, userType, _timer.ElapsedMilliseconds, request);
                }
            }
        }

        return result;
    }
}