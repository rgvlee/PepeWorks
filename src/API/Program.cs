using API.Common;
using API.Data;
using API.Extensions;
using API.Features.Locations;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PepeWorksContext>((serviceProvider, optionsBuilder) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    optionsBuilder.UseSqlServer(configuration.GetConnectionString("PepeWorks"));
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
    //This can be simplified by using a ResultBase type constraint on the pipeline behaviour, but I consider said option a smell
    //.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    .AddScoped<IPipelineBehavior<AddLocation.AddLocationCommand, Result<API.Domain.Location>>, ValidationBehaviour<AddLocation.AddLocationCommand, API.Domain.Location>>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    // o.CustomSchemaIds(type => type.ToString());
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.AddLocationRoutes();
app.Run();