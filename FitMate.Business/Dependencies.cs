using FitMate.Business.Interfaces;
using FitMate.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FitMate.Business
{
    internal static class Dependencies
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBodyweightRecordService, BodyweightRecordService>();
            services.AddScoped<IBodyweightTargetService, BodyweightTargetService>();
            services.AddScoped<IFoodRecordService, FoodRecordService>();
            services.AddScoped<ITimedGoalService, TimedGoalService>();
            services.AddScoped<IWeightliftingGoalService, WeightliftingGoalService>();
            services.AddScoped<ITimedProgressService, TimedProgressService>();
            services.AddScoped<IWeightliftingProgressService, WeightliftingProgressService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();

            return services;
        }
    }
}