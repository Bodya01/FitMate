using Microsoft.Extensions.DependencyInjection;

namespace FitMate.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Dependencies).Assembly);

        return services;
    }
}