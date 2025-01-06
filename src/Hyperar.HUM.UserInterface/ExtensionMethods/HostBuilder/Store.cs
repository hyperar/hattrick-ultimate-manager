namespace Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder
{
    using Hyperar.HUM.UserInterface.Store;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    internal static class Store
    {
        internal static IHostBuilder RegisterStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<ISessionStore, SessionStore>();
            });

            return host;
        }
    }
}