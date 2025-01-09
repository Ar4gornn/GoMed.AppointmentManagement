using GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Create.CreateAvailability;
using GoMed.AppointmentManagement.Application.Features.Availabilities.Commands.Update.UpdateAvailability;
using GoMed.AppointmentManagement.Application.Features.Availabilities.Dtos;
using GoMed.AppointmentManagement.Application.Features.Availabilities.Queries.GetAvailabilitiesByClinic;
using GoMed.AppointmentManagement.Application.Features.Availabilities.Set.SetAvailabilities;
using MediatR;

namespace GoMed.AppointmentManagement.WebApi.Endpoints;

public static class AvailabilityEndpoints
{
    public static void AddAvailabilityEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/availabilities")
            .WithTags("Availabilities")
            .WithOpenApi();

        // Existing endpoints ...

        // 1) GetUnavailabilityById all availabilities by clinic
        group.MapGet("/clinic/{clinicId:guid}", GetByClinic)
            .Produces<List<ReadAvailabilityDto>>(StatusCodes.Status200OK)
            .WithName("GetAvailabilitiesByClinic");

        // 2) Create an availability
        group.MapPost("/", Create)
            .Produces<int>(StatusCodes.Status201Created)
            .WithName("CreateAvailability");

        // 3) Update an existing availability
        group.MapPut("/", Update)
            .Produces(StatusCodes.Status200OK)
            .WithName("UpdateAvailability");

        // 4) Set multiple availabilities at once
        group.MapPost("/set", Set)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("SetAvailabilities");
    }

    private static async Task<IResult> GetByClinic(
        HttpContext context,
        IMediator mediator,
        Guid clinicId
    )
    {
        var request = new GetAvailabilitiesByClinic
        {
            ClinicId = clinicId
        };

        var response = await mediator.Send(request);
        return response.ToIResult();
    }

    private static async Task<IResult> Create(
        HttpContext context,
        IMediator mediator,
        CreateAvailability request
    )
    {
        var response = await mediator.Send(request);
        return response.ToIResult(StatusCodes.Status201Created);
    }

    private static async Task<IResult> Update(
        HttpContext context,
        IMediator mediator,
        UpdateAvailability request
    )
    {
        var response = await mediator.Send(request);
        return response.ToIResult();
    }

    /// <summary>
    /// Sets all availabilities for a given clinic in one go, replacing any existing ones.
    /// </summary>
    private static async Task<IResult> Set(
        HttpContext context,
        IMediator mediator,
        SetAvailabilities request
    )
    {
        var response = await mediator.Send(request);
        return response.ToIResult();
    }
}
