namespace Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    internal static class Mediatr
    {
        internal static IHostBuilder RegisterMediatR(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddMediatR((config) =>
                {
                    config.Lifetime = ServiceLifetime.Scoped;

                    config.RegisterServicesFromAssembly(
                        Assembly.Load("Hyperar.HUM.Application"));
                });
            });

            return host;
        }
    }
}