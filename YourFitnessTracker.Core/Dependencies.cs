using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourFitnessTracker.Core.Context;
using YourFitnessTracker.Core.Repositories.Implementations;
using YourFitnessTracker.Core.Repositories.Interfaces;
using YourFitnessTracker.Core.UnitOfWork;
using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Core
{
    internal static class Dependencies
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBodyweightRecordRepository, BodyweightRecordRepository>();
            services.AddScoped<IBodyweightTargetRepository, BodyweightTargetRepository>();
            services.AddScoped<IFoodRecordRepository, FoodRecordRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<INutritionTargetRepository, NutritionTargetRepository>();
            services.AddScoped<ITimedGoalRepository, TimedGoalRepository>();
            services.AddScoped<ITimedProgressRepository, TimedProgressRepository>();
            services.AddScoped<IWeightliftingGoalRepository, WeightliftingGoalRepository>();
            services.AddScoped<IWeightliftingProgressRepository, WeightliftingProgressRepository>();
            services.AddScoped<IWorkoutPlanRepository, WorkoutPlanRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }

        public static IServiceCollection RegisterContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<YourFitnessTrackerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection MigrateDatabase(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var context = provider.GetRequiredService<YourFitnessTrackerContext>();
            context.Database.Migrate();
            context.Database.EnsureCreated();

            return services;
        }

        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentity<FitnessUser, IdentityRole>()
                .AddEntityFrameworkStores<YourFitnessTrackerContext>()
                .AddSignInManager<SignInManager<FitnessUser>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}