using FitMate.Infrastructure.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(FitMate.Areas.Identity.IdentityHostingStartup))]
namespace FitMate.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddScoped<UserManager<FitnessUser>>();
                services.AddScoped<SignInManager<FitnessUser>>();
            });
        }
    }
}