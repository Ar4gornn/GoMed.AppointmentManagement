using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Create.CreateUnavailability;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Delete.DeleteUnavailability;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Command.Update.UpdateUnavailability;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Dtos;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Queries.GetUnavailabilitiesByClinic;
using GoMed.AppointmentManagement.Application.Features.Unavailabilities.Queries.GetUnavailabilityById;
using MediatR;

namespace GoMed.AppointmentManagement.WebApi.Endpoints;

public static class UnavailabilityEndpoints
{
    public static void AddUnavailabilityEndpoints(this IEndpointRouteBuilder builder)
    {
        // The group is set to "api/v1/unavailabilities"
        var group = builder.MapGroup("api/v1/unavailabilities")
            .WithTags("Unavailabilities")
            .WithOpenApi();

        // GET by clinic
        group.MapGet("/clinic/{clinicId:guid}", GetByClinic)
            .Produces<List<ReadUnavailabilityDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithName("GetUnavailabilitiesByClinic");

        // GET by Id
        group.MapGet("/{id:int}/clinic/{clinicId:guid}", GetById)
            .Produces<ReadUnavailabilityDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithName("GetUnavailabilityById");

        // CREATE
        group.MapPost("/", Create)
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("CreateUnavailability");

        // UPDATE
        group.MapPut("/{id:int}", Update)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("UpdateUnavailability");

        // DELETE
        group.MapDelete("/{id:int}/clinic/{clinicId:guid}", Delete)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithName("DeleteUnavailability");
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
        return response.ToIResult();
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
        return response.ToIResult();
    }

    private static async Task<IResult> Create(
        HttpContext context,
        IMediator mediator,
        CreateUnavailability request
    )
    {
        var response = await mediator.Send(request);
        // Optionally set a location header
        // context.Response.Headers.Location = $"/api/v1/unavailabilities/{response.Value}";
        return response.ToIResult(StatusCodes.Status201Created);
    }

    private static async Task<IResult> Update(
        HttpContext context,
        IMediator mediator,
        int id,
        UpdateUnavailability request
    )
    {
        // Optional: ensure route 'id' matches body 'request.Id'
        if (request.Id != id)
        {
            return Results.Problem(
                "Route parameter 'id' does not match the body request 'Id'.",
                statusCode: StatusCodes.Status400BadRequest
            );
        }
        
        var response = await mediator.Send(request);
        return response.ToIResult();
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
        return response.ToIResult();
    }
}
