using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Share.Web.Areas.Identity.IdentityHostingStartup))]
namespace Share.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}