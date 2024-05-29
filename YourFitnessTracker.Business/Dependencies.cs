using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace YourFitnessTracker.Business
{
    internal static class Dependencies
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBodyweightRecordService, BodyweightRecordService>();
            services.AddScoped<IBodyweightTargetService, BodyweightTargetService>();
            services.AddScoped<IFoodService, FoodService>();
            services.AddScoped<IFoodRecordService, FoodRecordService>();
            services.AddScoped<ITimedGoalService, TimedGoalService>();
            services.AddScoped<IWeightliftingGoalService, WeightliftingGoalService>();
            services.AddScoped<ITimedProgressService, TimedProgressService>();
            services.AddScoped<IWeightliftingProgressService, WeightliftingProgressService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();
            services.AddScoped<INutritionTargetService, NutritionTargetService>();

            return services;
        }
    }
}