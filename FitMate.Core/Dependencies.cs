using FitMate.Core.Context;
using FitMate.Core.Repositories.Implementations;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitMate.Core
{
    internal static class Dependencies
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {

            services.AddScoped<IBodyweightRecordRepository, BodyweightRecordRepository>();
            services.AddScoped<IBodyweightTargetRepository, BodyweightTargetRepository>();
            services.AddScoped<ITimedGoalRepository, TimedGoalRepository>();
            services.AddScoped<IWeightliftingGoalRepository, WeightliftingGoalRepository>();
            services.AddScoped<IWorkoutPlanRepository, WorkoutPlanRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IFoodRecordRepository, FoodRecordRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }

        public static IServiceCollection RegisterContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FitMateContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection MigrateDatabase(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var context = provider.GetRequiredService<FitMateContext>();
            context.Database.Migrate();
            context.Database.EnsureCreated();

            return services;
        }

        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentity<FitnessUser, IdentityRole>()
                .AddEntityFrameworkStores<FitMateContext>()
                .AddSignInManager<SignInManager<FitnessUser>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}