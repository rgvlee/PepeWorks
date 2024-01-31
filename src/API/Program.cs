using API.Data;
using API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PepeWorksContext>((serviceProvider, optionsBuilder) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    optionsBuilder.UseSqlServer(configuration.GetConnectionString("PepeWorks"));
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    // o.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.AddLocationRoutes();
app.Run();