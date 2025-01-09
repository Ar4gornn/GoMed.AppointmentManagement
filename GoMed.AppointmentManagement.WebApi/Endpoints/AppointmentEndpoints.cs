using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Approve;
using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Cancel;
using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Create.CreateAppointmentCommand;
using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Delete.DeleteAppointmentCommand;
using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Reschedule;
using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Set.SetAppointmentShowedUpCommand;
using GoMed.AppointmentManagement.Application.Features.Appointments.Command.Update;
using GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByClinicIdQuery;
using GoMed.AppointmentManagement.Application.Features.Appointments.Queries.Get.GetAppointmentByPatientIdQuery;
using GoMed.AppointmentManagement.Application.Features.Appointments.Dtos;
using MediatR;

namespace GoMed.AppointmentManagement.WebApi.Endpoints;

public static class AppointmentEndpoints
{
    public static void AddAppointmentEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/appointments")
            .WithTags("Appointments")
            .WithOpenApi();

        // Get appointments by ClinicId
        group.MapGet("/clinic/{clinicId}", GetByClinicId)
            .Produces<List<ReadAppointmentDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("GetAppointmentsByClinicId");

        // Get appointments by PatientId
        group.MapGet("/patient/{patientId}", GetByPatientId)
            .Produces<List<ReadAppointmentDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("GetAppointmentsByPatientId");

        // Create a new appointment
        group.MapPost("/", Create)
            .Produces<ReadAppointmentDto>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("CreateAppointment");

        // Update an appointment
        group.MapPut("/{id}", Update)
            .Produces<ReadAppointmentDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithName("UpdateAppointment");

        // Delete an appointment
        group.MapDelete("/{id}", Delete)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithName("DeleteAppointment");

        // Reschedule an appointment
        group.MapPut("/{id}/reschedule", Reschedule)
            .Produces<ReadAppointmentDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("RescheduleAppointment");

        // Approve or Decline an appointment
        group.MapPut("/{id}/approve", Approve)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("ApproveAppointment");

        // Set Appointment Showed Up
        group.MapPut("/{id}/showed-up", SetShowedUp)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("SetAppointmentShowedUp");

        // Cancel an appointment
        group.MapPost("/{id}/cancel", Cancel)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("CancelAppointment");
    }

    // Get Appointments by ClinicId
    private static async Task<IResult> GetByClinicId(
        HttpContext context,
        IMediator mediator,
        Guid clinicId,
        DateTimeOffset startDate,
        DateTimeOffset endDate
    )
    {
        var request = new GetAppointmentByClinicIdQuery(clinicId, startDate, endDate);
        var response = await mediator.Send(request);
        return Results.Ok(response);
    }

    // Get Appointments by PatientId
    private static async Task<IResult> GetByPatientId(
        HttpContext context,
        IMediator mediator,
        Guid patientId
    )
    {
        var request = new GetAppointmentByPatientIdQuery(patientId);
        var response = await mediator.Send(request);
        return Results.Ok(response);
    }

    // Create Appointment
    private static async Task<IResult> Create(
        HttpContext context,
        IMediator mediator,
        CreateAppointmentDto requestDto
    )
    {
        var command = new CreateAppointmentCommand(requestDto);
        var response = await mediator.Send(command);
        return Results.Created($"/api/v1/appointments/{response.ClinicId}", response);
    }

    // Update Appointment
    private static async Task<IResult> Update(
        HttpContext context,
        IMediator mediator,
        Guid id,
        UpdateAppointmentDto requestDto
    )
    {
        if (requestDto.Id != id)
        {
            return Results.Problem(
                "Route parameter 'id' does not match the body 'Id'.",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var command = new UpdateAppointmentCommand(requestDto);
        var response = await mediator.Send(command);
        return Results.Ok(response);
    }

    // Delete Appointment
    private static async Task<IResult> Delete(
        HttpContext context,
        IMediator mediator,
        Guid id
    )
    {
        var command = new DeleteAppointmentCommand(id);
        await mediator.Send(command);
        return Results.Ok();
    }

    // Reschedule Appointment
    private static async Task<IResult> Reschedule(
        HttpContext context,
        IMediator mediator,
        Guid id,
        DateTimeOffset startAt,
        DateTimeOffset endAt
    )
    {
        var command = new RescheduleAppointmentCommand(id, startAt, endAt);
        await mediator.Send(command);
        return Results.Ok();
    }

    // Approve Appointment
    private static async Task<IResult> Approve(
        HttpContext context,
        IMediator mediator,
        Guid id,
        bool isApproved
    )
    {
        var command = new ApproveAppointmentCommand(id, isApproved);
        await mediator.Send(command);
        return Results.Ok();
    }

    // Set Appointment Showed Up
    private static async Task<IResult> SetShowedUp(
        HttpContext context,
        IMediator mediator,
        Guid id,
        bool showedUp
    )
    {
        var command = new SetAppointmentShowedUpCommand(id, showedUp);
        await mediator.Send(command);
        return Results.Ok();
    }

    // Cancel Appointment
    private static async Task<IResult> Cancel(
        HttpContext context,
        IMediator mediator,
        Guid id,
        CancelAppointmentDto requestDto
    )
    {
        if (requestDto.AppointmentId != id)
        {
            return Results.Problem(
                "Route parameter 'id' does not match the body 'AppointmentId'.",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var command = new CancelAppointmentCommand(requestDto);
        await mediator.Send(command);
        return Results.Ok();
    }
}
