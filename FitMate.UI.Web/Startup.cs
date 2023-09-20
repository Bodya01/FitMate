using FitMate.Infrastructure.Entities;
using FitMate.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using FitMate.Applcation.ServiceCollectionExtensions;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Core.Repositories.Implementations;
using FitMate.Core.UnitOfWork;

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
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<FitnessUser, IdentityRole>()
                .AddEntityFrameworkStores<FitMateContext>()
                .AddSignInManager<SignInManager<FitnessUser>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddMediatRAndHandlers();

            services.AddTransient<IBodyweightRepository, BodyweightRepository>();
            services.AddTransient<IGoalRepository, GoalRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
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