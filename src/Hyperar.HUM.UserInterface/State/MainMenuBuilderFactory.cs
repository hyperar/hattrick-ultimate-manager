namespace Hyperar.HUM.UserInterface.State
{
    using System;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    internal class MainMenuBuilderFactory : IMainMenuBuilderFactory
    {
        private readonly INavigator navigator;

        private readonly IServiceScopeFactory serviceScopeFactory;

        public MainMenuBuilderFactory(
            INavigator navigator,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.navigator = navigator;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public IMainMenuBuilderStrategy GetMainMenuBuilderAsync()
        {
            throw new NotImplementedException();
        }
    }
}