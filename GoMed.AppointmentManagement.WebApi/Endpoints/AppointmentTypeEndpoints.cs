using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Create.CreateAppointmentType;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Delete.DeleteAppointmentType;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Commands.Update.UpdateAppointmentType;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Dtos;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.Get.GetAppointmentTypeById;
using GoMed.AppointmentManagement.Application.Features.AppointmentTypes.Queries.GetAll.GetAllAppointmentTypesQueries;
using MediatR;

namespace GoMed.AppointmentManagement.WebApi.Endpoints;

public static class AppointmentTypeEndpoints
{
    public static void AddAppointmentTypeEndpoints(this IEndpointRouteBuilder builder)
    {
        // The group is set to "api/v1/appointment-types"
        // Adjust as needed for your URL structure
        var group = builder.MapGroup("api/v1/appointment-types")
            .WithTags("AppointmentTypes")
            .WithOpenApi();

        // GetUnavailabilityById all appointment types by clinic
        group.MapGet("/", GetAll)
            .Produces<List<ReadAppointmentTypeDto>>(StatusCodes.Status200OK)
            .WithName("GetAllAppointmentTypes");

        // GetUnavailabilityById an appointment type by Id
        group.MapGet("/{id}", GetById)
            .Produces<ReadAppointmentTypeDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithName("GetAppointmentTypeById");

        // Create an appointment type
        group.MapPost("/", Create)
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("CreateAppointmentType");

        // Update an appointment type
        group.MapPut("/{id}", Update)
            .Produces<int>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("UpdateAppointmentType");

        // Delete an appointment type
        group.MapDelete("/{id}", Delete)
            .Produces<int>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithName("DeleteAppointmentType");
    }

    /// <summary>
    ///     Gets all appointment types filtered by ClinicId
    ///     If ClinicId is not provided, will return all.
    /// </summary>
    private static async Task<IResult> GetAll(
        HttpContext context,
        IMediator mediator,
        [AsParameters] Guid clinicId = default
    )
    {
        var request = new GetAllAppointmentTypes
        {
            ClinicId = clinicId
        };
        
        var response = await mediator.Send(request);
        return response.ToIResult();
    }

    /// <summary>
    ///     Gets the appointment type for the specified id
    /// </summary>
    private static async Task<IResult> GetById(
        HttpContext context,
        IMediator mediator,
        int id
    )
    {
        var request = new GetAppointmentTypeById
        {
            Id = id
        };
        
        var response = await mediator.Send(request);
        return response.ToIResult();
    }

    /// <summary>
    ///     Creates a new appointment type.
    /// </summary>
    private static async Task<IResult> Create(
        HttpContext context,
        IMediator mediator,
        CreateAppointmentType request
    )
    {
        var response = await mediator.Send(request);
        // If successful, you could optionally include a location header for the newly created resource:
        //   context.Response.Headers.Location = $"/api/v1/appointment-types/{response.Value}";
        return response.ToIResult(StatusCodes.Status201Created);
    }

    /// <summary>
    ///     Updates an existing appointment type by id
    /// </summary>
    private static async Task<IResult> Update(
        HttpContext context,
        IMediator mediator,
        int id,
        UpdateAppointmentType request
    )
    {
        // Ensure the request Id matches the route Id (optional consistency check)
        if (request.Id != id)
        {
            // You can handle this however you'd like. Returning a 400 is typical.
            return Results.Problem(
                "The route parameter 'id' does not match the body request 'Id'.",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var response = await mediator.Send(request);
        return response.ToIResult();
    }

    /// <summary>
    ///     Deletes an appointment type by id in the specified clinic.
    /// </summary>
    private static async Task<IResult> Delete(
        HttpContext context,
        IMediator mediator,
        int id,
        [AsParameters] Guid clinicId
    )
    {
        var request = new DeleteAppointmentType
        {
            Id = id,
            ClinicId = clinicId
        };

        var response = await mediator.Send(request);
        return response.ToIResult();
    }
}