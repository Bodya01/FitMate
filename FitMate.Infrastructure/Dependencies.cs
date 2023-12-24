using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FitMate.Infrastructure;

public static class Dependencies
{
    //TODO: Refactor
    private const string CoreLayerPath = "../FitMate.Core/bin/Debug/net7.0/FitMate.Core.dll";
    private const string BusinessLayerPath = "../FitMate.Business/bin/Debug/net7.0/FitMate.Business.dll";

    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Dependencies).Assembly);

        return services;
    }

    public static IServiceCollection RegisterContext(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.LoadFrom(CoreLayerPath);
        var type = assembly.GetType("FitMate.Core.Dependencies");
        var methodInfo = type?.GetMethod("RegisterContext");
        methodInfo?.Invoke(null, new object[] { services, configuration });

        return services;
    }

    public static IServiceCollection MigrateDatabase(this IServiceCollection services)
    {
        var assembly = Assembly.LoadFrom(CoreLayerPath);
        var type = assembly.GetType("FitMate.Core.Dependencies");
        var methodInfo = type?.GetMethod("MigrateDatabase");
        methodInfo?.Invoke(null, new object[] { services });

        return services;
    }
    public static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {
        var assembly = Assembly.LoadFrom(CoreLayerPath);
        var type = assembly.GetType("FitMate.Core.Dependencies");
        var methodInfo = type?.GetMethod("RegisterIdentity");
        methodInfo?.Invoke(null, new object[] { services });

        return services;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        var assembly = Assembly.LoadFrom(CoreLayerPath);
        var type = assembly.GetType("FitMate.Core.Dependencies");
        var methodInfo = type?.GetMethod("RegisterRepositories");
        methodInfo?.Invoke(null, new object[] { services });

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        var assembly = Assembly.LoadFrom(BusinessLayerPath);
        var type = assembly.GetType("FitMate.Business.Dependencies");
        var methodInfo = type?.GetMethod("RegisterServices");
        methodInfo?.Invoke(null, new object[] { services });

        return services;
    }
}