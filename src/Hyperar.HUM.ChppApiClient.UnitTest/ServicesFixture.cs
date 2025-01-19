namespace Hyperar.HUM.ChppApiClient.UnitTest
{
    using System;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class ServicesFixture
    {
        public ServicesFixture()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>((services) => new ConfigurationBuilder().AddJsonFile("appSettings.json").Build());

            services.AddSingleton<IProtectedResourceUrlFactory, ProtectedResourceUrlFactory>();

            this.Services = services.BuildServiceProvider();
        }

        public IServiceProvider Services { get; private set; }
    }
}