namespace Hyperar.HUM.UserInterface.State
{
    using System.Linq;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.UserProfile.Queries.List;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    internal class LandingPageFactory : ILandingViewFactory
    {
        private readonly INavigator navigator;

        private readonly IServiceScopeFactory serviceScopeFactory;

        private readonly ISessionStore sessionStore;

        public LandingPageFactory(
            INavigator navigator,
            IServiceScopeFactory serviceScopeFactory,
            ISessionStore sessionStore)
        {
            this.navigator = navigator;
            this.serviceScopeFactory = serviceScopeFactory;
            this.sessionStore = sessionStore;
        }

        public async Task<ViewType> GetLandingViewAsync()
        {
            using (var scope = this.serviceScopeFactory.CreateScope())
            {
                var landingViewType = ViewType.Home;

                var sender = scope.ServiceProvider.GetRequiredService<ISender>();

                var userProfiles = await sender.Send(
                    new ListUserProfilesQuery());

                if (userProfiles is not null && userProfiles.Count() == 1)
                {
                    var userProfile = userProfiles.Single();

                    this.sessionStore.SetSelectedUserProfile(userProfile.Id);

                    if (!userProfile.HasAuthorized)
                    {
                        landingViewType = ViewType.Authorization;
                    }
                    else if (userProfile.LastDownloadDate is null)
                    {
                        landingViewType = ViewType.Download;
                    }
                    else if (userProfile.SelectedTeamHattrickId is null)
                    {
                        landingViewType = ViewType.TeamSelection;
                    }
                }
                else
                {
                    landingViewType = ViewType.UserProfileSelection;
                }

                return landingViewType;
            }
        }
    }
}