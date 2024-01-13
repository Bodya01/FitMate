using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FitMate.Infrastructure;

public static class Dependencies
{
    // Might be a bit over complicated
    private static class Core
    {
        public const string Path = "FitMate.Core";
        public const string Dependencies = "FitMate.Core.Dependencies";

        public static class Methods
        {
            public const string RegisterContext = "RegisterContext";
            public const string MigrateDatabase = "MigrateDatabase";
            public const string RegisterIdentity = "RegisterIdentity";
            public const string RegisterRepositories = "RegisterRepositories";
        }
    }

    private static class Business
    {
        public const string Path = "FitMate.Business";
        public const string Dependencies = "FitMate.Business.Dependencies";

        public static class Methods
        {
            public const string RegisterServices = "RegisterServices";
        }
    }

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
        InvokeDependencyMethod(Core.Path, Core.Dependencies, Core.Methods.RegisterContext, services, configuration);
        return services;
    }

    public static IServiceCollection MigrateDatabase(this IServiceCollection services)
    {
        InvokeDependencyMethod(Core.Path, Core.Dependencies, Core.Methods.MigrateDatabase, services);
        return services;
    }

    public static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {
        InvokeDependencyMethod(Core.Path, Core.Dependencies, Core.Methods.RegisterIdentity, services);
        return services;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        InvokeDependencyMethod(Core.Path, Core.Dependencies, Core.Methods.RegisterRepositories, services);
        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        InvokeDependencyMethod(Business.Path, Business.Dependencies, Business.Methods.RegisterServices, services);
        return services;
    }
}