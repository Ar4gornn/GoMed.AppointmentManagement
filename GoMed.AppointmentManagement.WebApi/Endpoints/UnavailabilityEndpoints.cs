using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Create;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Delete;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Update;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Dtos;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Queries.GetUnavailabilitiesByClinic;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Queries.GetUnavailabilityById;
using MediatR;

namespace GoMed.AppointmentManagement.WebApi.Endpoints;

public static class UnavailabilityEndpoints
{
    public static void AddUnavailabilityEndpoints(this IEndpointRouteBuilder builder)
    {
        // Clinic-specific unavailability endpoints
        var clinicGroup = builder.MapGroup("api/v1/clinics/{clinicId:guid}/unavailabilities")
            .WithTags("Clinic Unavailabilities")
            .WithOpenApi();

        // GET all unavailabilities for a clinic
        clinicGroup.MapGet("", GetByClinic)  // no extra route segment
            .Produces<List<ReadUnavailabilityDto>>(StatusCodes.Status200OK);

        // GET unavailability by Id
        clinicGroup.MapGet("/{id:int}", GetById)  // just /{id:int}
            .Produces<ReadUnavailabilityDto>(StatusCodes.Status200OK);

        // CREATE unavailability
        clinicGroup.MapPost("", Create)           // no extra route segment
            .Produces<int>(StatusCodes.Status201Created);

        // UPDATE unavailability
        clinicGroup.MapPut("/{id:int}", Update)   // just /{id:int}
            .Produces(StatusCodes.Status200OK);

        // DELETE unavailability
        clinicGroup.MapDelete("/{id:int}", Delete)  // just /{id:int}
            .Produces(StatusCodes.Status200OK);

    }

    private static async Task<IResult> GetByClinic(
        HttpContext context,
        IMediator mediator,
        Guid clinicId 
    )
    {
        var request = new GetUnavailabilitiesByClinic
        {
            ClinicId = clinicId
            
        };

        var response = await mediator.Send(request);
        return response.IsSuccess
            ? Results.Ok(response.Value)
            : response.ToIResult();
    }

    private static async Task<IResult> GetById(
        HttpContext context,
        IMediator mediator,
        int id,       
        Guid clinicId 
    )
    {
        var request = new GetUnavailabilityById
        {
            Id = id,
            ClinicId = clinicId
        };

        var response = await mediator.Send(request);
        return response.IsSuccess
            ? Results.Ok(response.Value)
            : response.ToIResult();
    }

    private static async Task<IResult> Create(
        HttpContext context,
        IMediator mediator,
        CreateUnavailability request
    )
    {
        var response = await mediator.Send(request);
        if (!response.IsSuccess) return response.ToIResult();
        return Results.Created("/api/v1/unavailabilities", response.Value);
    }

    private static async Task<IResult> Update(
        HttpContext context,
        IMediator mediator,
        int id,
        UpdateUnavailability request
    )
    {
        if (request.Id != id)
        {
            return Results.Problem(
                "Route parameter 'id' does not match the body request 'Id'.",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var response = await mediator.Send(request);
        if (!response.IsSuccess) return response.ToIResult();
        return Results.Ok();
    }

    private static async Task<IResult> Delete(
        HttpContext context,
        IMediator mediator,
        int id,
        Guid clinicId
    )
    {
        var request = new DeleteUnavailability
        {
            Id = id,
            ClinicId = clinicId
        };

        var response = await mediator.Send(request);
        if (!response.IsSuccess) return response.ToIResult();
        return Results.Ok();
    }
}
