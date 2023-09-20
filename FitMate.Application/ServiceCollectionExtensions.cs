using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FitMate.Applcation.ServiceCollectionExtensions
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