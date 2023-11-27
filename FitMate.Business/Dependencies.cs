using FitMate.Business.Interfaces;
using FitMate.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FitMate.Business
{
    internal static class Dependencies
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();

            return services;
        }
    }
}