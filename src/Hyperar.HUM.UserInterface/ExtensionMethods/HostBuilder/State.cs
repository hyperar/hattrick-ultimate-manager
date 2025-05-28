namespace Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder
{
    using Hyperar.HUM.UserInterface.State;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    internal static class State
    {
        internal static IHostBuilder RegisterStateObjects(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<ILandingViewFactory, LandingPageFactory>();
                services.AddSingleton<INavigator, Navigator>();
            });

            return host;
        }
    }
}