using API.Domain;
using API.Features.Locations;
using MediatR;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class LocationExtensions
{
    public static WebApplication AddLocationRoutes(this WebApplication webApplication)
    {
        webApplication.MapGet("/locations", async (IMediator mediator) =>
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

        webApplication.MapGet("/locations/{id}", async (IMediator mediator, Guid id) =>
            {
                var location = await mediator.Send(new GetLocationById.GetLocationByIdQuery { Id = id });
                return location is not null ? Results.Ok(location) : Results.NotFound();
            })
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Description = "Gets an existing location."
            })
            .WithTags("Locations")
            .Produces<Location>();

        webApplication.MapPost("/locations", async (IMediator mediator, AddLocation.AddLocationCommand command) =>
            {
                var location = await mediator.Send(command);
                return Results.Created($"/locations/{location.Id}", location);
            })
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Description = "Adds a new location."
            })
            .WithTags("Locations")
            .Produces<Location>();

        webApplication.MapDelete("/locations/{id}", async (IMediator mediator, Guid id) =>
            {
                await mediator.Send(new DeleteLocationById.DeleteLocationByIdCommand { Id = id });
                return Results.NoContent();
            })
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Description = "Deletes an existing location."
            })
            .WithTags("Locations");

        return webApplication;
    }
}