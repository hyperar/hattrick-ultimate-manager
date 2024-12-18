namespace Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    internal static class Configuration
    {
        private const string BaseConfigurationFile = "appSettings.json";

        private const string DevelopmentConfigurationFile = "appSettings.debug.json";

        private const string KeysConfigurationFile = "appSettings.debug.json";

        private const string ProductionConfigurationFile = "appSettings.production.json";

        internal static IHostBuilder RegisterConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                configurationBuilder.AddJsonFile(BaseConfigurationFile);
                configurationBuilder.AddJsonFile(KeysConfigurationFile);

                if (context.HostingEnvironment.IsDevelopment())
                {
                    configurationBuilder.AddJsonFile(DevelopmentConfigurationFile);
                }
                else if (context.HostingEnvironment.IsProduction())
                {
                    configurationBuilder.AddJsonFile(ProductionConfigurationFile);
                }
            });

            return host;
        }
    }
}