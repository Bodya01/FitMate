using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(FitMate.Areas.Identity.IdentityHostingStartup))]
namespace FitMate.Areas.Identity
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