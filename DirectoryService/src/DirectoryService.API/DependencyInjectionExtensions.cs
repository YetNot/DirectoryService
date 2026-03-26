using DirectoryService.Application;
using DirectoryService.Infrastructure.Postgres;
using DirectoryService.Presentation.Envelopes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using SharedKernel;

namespace DirectoryService.Presentation;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection
        AddConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddSerilogLogging(configuration)
            .AddInfrastructurePostgres(configuration)
            .AddApplication()
            .AddApi();

    private static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddOpenApi(options =>
        {
            options.AddSchemaTransformer((schema, context, _) =>
            {
                if (context.JsonTypeInfo.Type == typeof(Envelope<Errors>))
                {
                    if (schema.Properties.TryGetValue("errors", out var errorsProp))
                    {
                        errorsProp.Items.Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "Error", };
                    }
                }

                return Task.CompletedTask;
            });
        });

        return services;
    }

    private static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog((sp, lc) => lc
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(sp)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ServiceName", "DirectoryService"));

        return services;
    }
}