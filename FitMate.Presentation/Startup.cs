using FitMate.Applcation.ServiceCollectionExtensions;
using FitMate.Business.Interfaces;
using FitMate.Business.Services;
using FitMate.Core.Context;
using FitMate.Core.Repositories.Implementations;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure;
using FitMate.Infrastructure.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace FitMate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FitMateContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<FitnessUser, IdentityRole>()
                .AddEntityFrameworkStores<FitMateContext>()
                .AddSignInManager<SignInManager<FitnessUser>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddMediatRAndHandlers();
            Dependencies.RegisterInfrastructure(services);

            services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();

            services.AddScoped<IBodyweightRecordRepository, BodyweightRecordRepository>();
            services.AddScoped<IBodyweightTargetRepository, BodyweightTargetRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<IGoalProgressRepository, GoalProgressRepository>();
            services.AddScoped<IWorkoutPlanRepository, WorkoutPlanRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IFoodRecordRepository, FoodRecordRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var cultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("de"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = cultures,
                SupportedUICultures = cultures
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Bodyweight}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}