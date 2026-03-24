using System.Reflection;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Locations.CreateLocation;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = typeof(DependencyInjectionExtensions).Assembly;

        services.Scan(scan => scan.FromAssemblies([assembly])
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }
}