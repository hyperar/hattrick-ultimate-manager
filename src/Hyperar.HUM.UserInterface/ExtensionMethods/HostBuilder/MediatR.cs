namespace Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder
{
    using Hyperar.HUM.Infrastructure.Database;
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
                        typeof(DatabaseContext)
                            .Assembly);
                });
            });

            return host;
        }
    }
}