using System.Net;
using System.Text.Json;
using API.Domain;
using API.Features.Locations;
using MediatR;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class LocationExtensions
{
    public static WebApplication AddLocationRoutes(this WebApplication webApplication)
    {
        webApplication.MapGet("/locations", async (ISender mediator) =>
            {
                var locations = await mediator.Send(new GetAllLocations.GetAllLocationsQuery());
                return Results.Ok(locations);
            })
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Description = "Gets all current locations."
            })
            .WithTags("Locations")
            .Produces<IEnumerable<Location>>();

        webApplication.MapGet("/locations/{id}", async (ISender mediator, Guid id) =>
            {
                var location = await mediator.Send(new GetLocationById.GetLocationByIdQuery { Id = id });
                return location is not null ? Results.Ok(location) : Results.NotFound();
            })
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Description = "Gets an existing location."
            })
            .WithTags("Locations")
            .Produces<Location>()
            .Produces((int)HttpStatusCode.NotFound);

        webApplication.MapPost("/locations", async (ISender mediator, AddLocation.AddLocationCommand command) =>
            {
                var result = await mediator.Send(command);
                return result.IsSuccess
                    ? Results.Created($"/locations/{result.Value.Id}", result.Value)
                    : Results.BadRequest(JsonSerializer.Serialize(result.Errors.Select(x => x.Message)));
            })
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Description = "Adds a new location."
            })
            .WithTags("Locations")
            .Produces<Location>((int)HttpStatusCode.Created);

        webApplication.MapDelete("/locations/{id}", async (ISender mediator, Guid id) =>
            {
                await mediator.Send(new DeleteLocationById.DeleteLocationByIdCommand { Id = id });
                return Results.NoContent();
            })
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Description = "Deletes an existing location."
            })
            .WithTags("Locations")
            .Produces((int)HttpStatusCode.NoContent);

        return webApplication;
    }
}