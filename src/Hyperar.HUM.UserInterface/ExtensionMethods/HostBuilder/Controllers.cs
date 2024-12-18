namespace Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    internal static class Controllers
    {
        internal static IHostBuilder RegisterControllers(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddScoped<UserProfileController>();
            });

            return host;
        }
    }
}