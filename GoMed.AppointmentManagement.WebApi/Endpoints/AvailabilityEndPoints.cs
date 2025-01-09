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
        // Clinic-related endpoints
        var clinicGroup = builder.MapGroup("api/v1/availabilities/clinics")
            .WithTags("Clinic Availabilities")
            .WithOpenApi();

        // 1) Get all availabilities by clinic
        clinicGroup.MapGet("/{clinicId:guid}", GetByClinic)
            .Produces<List<ReadAvailabilityDto>>(StatusCodes.Status200OK);

        // 2) Set multiple availabilities at once
        clinicGroup.MapPost("/set", Set)
            .Produces(StatusCodes.Status200OK);
        

        // 3) Create an availability
        clinicGroup.MapPost("/", Create)
            .Produces<int>(StatusCodes.Status201Created);

        // 4) Update an existing availability
        clinicGroup.MapPut("/", Update)
            .Produces(StatusCodes.Status200OK);
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

