using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace YourFitnessTracker.Applcation.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMediatRAndHandlers(this IServiceCollection services)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(m =>
            {
                m.RegisterServicesFromAssembly(currentAssembly);
            });

            return services;
        }
    }
}