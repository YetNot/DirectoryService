using DirectoryService.Application;
using DirectoryService.Application.Locations;
using DirectoryService.Infrastructure.Postgres;
using DirectoryService.Infrastructure.Postgres.Locations;
using DirectoryService.Presentation.Envelopes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SharedKernel;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddOpenApi(options =>
{
    options.AddSchemaTransformer((schema, context, _) =>
    {
        if (context.JsonTypeInfo.Type == typeof(Envelope<Errors>))
        {
            if (schema.Properties.TryGetValue("errors", out var errorsProp))
            {
                errorsProp.Items.Reference = new OpenApiReference
                {
                    Type = ReferenceType.Schema,
                    Id = "Error",
                };
            }
        }

        return Task.CompletedTask;
    });
});

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