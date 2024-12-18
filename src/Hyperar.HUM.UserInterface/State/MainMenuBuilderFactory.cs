namespace Hyperar.HUM.UserInterface.State
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.Controllers;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.State.Strategies.MainMenuBuilder;
    using Microsoft.Extensions.DependencyInjection;

    internal class MainMenuBuilderFactory : IMainMenuBuilderFactory
    {
        private readonly NoUserProfileFound noUserProfileFoundStrategy;

        private readonly IServiceScopeFactory serviceScopeFactory;

        public MainMenuBuilderFactory(
            IServiceScopeFactory serviceScopeFactory,
            NoUserProfileFound noUserProfileFoundStrategy)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.noUserProfileFoundStrategy = noUserProfileFoundStrategy;
        }

        public async Task<IMainMenuBuilderStrategy> GetBuilderAsync()
        {
            var userProfileController = this.serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<UserProfileController>();

            var userProfiles = await userProfileController.ListAsync();

            return this.noUserProfileFoundStrategy;
        }
    }
}