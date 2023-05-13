using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(FitnessTracker.Areas.Identity.IdentityHostingStartup))]
namespace FitnessTracker.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}