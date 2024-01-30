using API.Data;
using API.Features.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PepeWorksContext>((serviceProvider, optionsBuilder) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    optionsBuilder.UseSqlServer(configuration.GetConnectionString("PepeWorks"));
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.MapPost("/locations", async (IMediator mediator, AddLocation.AddLocationCommand command) =>
    {
        var location = await mediator.Send(command);
        return Results.Created($"/locations/{location.Id}", location);
    })
    .WithOpenApi(operation => new OpenApiOperation(operation)
    {
        Description = "Adds a new location."
    })
    .WithTags("Locations")
    .Produces<AddLocation.Location>();
app.Run();