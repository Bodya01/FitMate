using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using YourFitnessTracker.Infrastructure.Entities;

[assembly: HostingStartup(typeof(YourFitnessTracker.Areas.Identity.IdentityHostingStartup))]
namespace YourFitnessTracker.Areas.Identity
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