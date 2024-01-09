using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FitMate.Infrastructure;

public static class Dependencies
{
    private static string GetAssemblyPath(string assemblyName)
    {
        var executingAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        return Path.Combine(executingAssemblyPath, $"{assemblyName}.dll");
    }

    private static void InvokeDependencyMethod(string assemblyName, string typeName, string methodName, IServiceCollection services, IConfiguration configuration = null)
    {
        var assemblyPath = GetAssemblyPath(assemblyName);
        var assembly = Assembly.LoadFrom(assemblyPath);
        var type = assembly.GetType(typeName);
        var methodInfo = type?.GetMethod(methodName);

        if (methodInfo is not null)
        {
            var parameters = configuration is not null ? new object[] { services, configuration } : new object[] { services };
            methodInfo.Invoke(null, parameters);
        }
    }

    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Dependencies).Assembly);
        return services;
    }

    public static IServiceCollection RegisterContext(this IServiceCollection services, IConfiguration configuration)
    {
        InvokeDependencyMethod("FitMate.Core", "FitMate.Core.Dependencies", "RegisterContext", services, configuration);
        return services;
    }

    public static IServiceCollection MigrateDatabase(this IServiceCollection services)
    {
        InvokeDependencyMethod("FitMate.Core", "FitMate.Core.Dependencies", "MigrateDatabase", services);
        return services;
    }

    public static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {
        InvokeDependencyMethod("FitMate.Core", "FitMate.Core.Dependencies", "RegisterIdentity", services);
        return services;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        InvokeDependencyMethod("FitMate.Core", "FitMate.Core.Dependencies", "RegisterRepositories", services);
        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        InvokeDependencyMethod("FitMate.Business", "FitMate.Business.Dependencies", "RegisterServices", services);
        return services;
    }
}