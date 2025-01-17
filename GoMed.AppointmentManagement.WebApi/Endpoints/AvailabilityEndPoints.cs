using GoMed.AppointmentManagement.Application.Features.Availabilities.Get.GetAvailabilitiesByClinic;
using GoMed.AppointmentManagement.Application.Features.Availabilities.Set.SetAvailabilities;
using GoMed.AppointmentManagement.Domain.Entities;
using MediatR;

namespace GoMed.AppointmentManagement.WebApi.Endpoints;

public static class AvailabilityEndpoints
{
    public static void AddAvailabilityEndpoints(this IEndpointRouteBuilder builder)
    {
        // Clinic-related endpoints
        var clinicGroup = builder.MapGroup("api/v1/clinics/{clinicId:guid}/availabilities")
            .WithTags("Clinic Availabilities")
            .WithOpenApi();
        

        // 2) Set multiple availabilities at once
        clinicGroup.MapPost("/", Set)
            .Produces(StatusCodes.Status200OK);

        clinicGroup.MapGet("/", GetByClinic)
            .Produces<List<Availability>>();
    }

    /// <summary>
    /// 1) Retrieves all availabilities for the specified clinic.
    /// </summary>
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

    /// <summary>
    /// 4) Sets multiple availabilities for a clinic at once, 
    ///    replacing any that are omitted in the incoming list.
    /// </summary>
    private static async Task<IResult> Set(
        HttpContext context,
        IMediator mediator,
        SetAvailabilities request,
        Guid clinicId
    )
    {
        var response = await mediator.Send(request);
        return response.ToIResult();
    }
}
