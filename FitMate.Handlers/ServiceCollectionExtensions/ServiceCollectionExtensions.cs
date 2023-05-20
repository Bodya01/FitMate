using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FitMate.Handlers.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMediatRAndHandlers(this IServiceCollection services)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(m =>
            {
                m.RegisterServicesFromAssembly(currentAssembly);
            });
        }
    }

}