using DirectoryService.Application;
using DirectoryService.Application.Locations;
using DirectoryService.Infrastructure.Postgres;
using DirectoryService.Infrastructure.Postgres.Locations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddInfrastructurePostgres(builder.Configuration);

builder.Services.AddScoped<ILocationsRepository, LocationsRepository>();

builder.Services.AddApplication();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "DirectoryService API"));
}

app.MapControllers();

app.Run();