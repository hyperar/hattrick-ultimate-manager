namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.UserInterface.ViewModels.Interfaces;

    internal delegate Task<TViewModel> CreateViewModelAsync<TViewModel>() where TViewModel : ViewModelBase;

    internal class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModelAsync<AuthorizationViewModel> createAuthorizationViewModelAsync;

        private readonly CreateViewModelAsync<DownloadViewModel> createDownloadViewModelAsync;

        private readonly CreateViewModelAsync<HomeViewModel> createHomeViewModelAsync;

        private readonly CreateViewModelAsync<TeamSelectionViewModel> createTeamSelectionViewModelAsync;

        private readonly CreateViewModelAsync<UserProfileSelectionViewModel> createUserProfileSelectionViewModelAsync;

        public ViewModelFactory(
            CreateViewModelAsync<AuthorizationViewModel> createAuthorizationViewModelAsync,
            CreateViewModelAsync<DownloadViewModel> createDownloadViewModelAsync,
            CreateViewModelAsync<HomeViewModel> createHomeViewModelAsync,
            CreateViewModelAsync<UserProfileSelectionViewModel> createUserProfileSelectionViewModelAsync,
            CreateViewModelAsync<TeamSelectionViewModel> createTeamSelectionViewModelAsync)
        {
            this.createAuthorizationViewModelAsync = createAuthorizationViewModelAsync;
            this.createDownloadViewModelAsync = createDownloadViewModelAsync;
            this.createHomeViewModelAsync = createHomeViewModelAsync;
            this.createTeamSelectionViewModelAsync = createTeamSelectionViewModelAsync;
            this.createUserProfileSelectionViewModelAsync = createUserProfileSelectionViewModelAsync;
        }

        public async Task<ViewModelBase> CreateViewModelAsync(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Authorization => await this.createAuthorizationViewModelAsync(),
                ViewType.Download => await this.createDownloadViewModelAsync(),
                ViewType.Home => await this.createHomeViewModelAsync(),
                ViewType.TeamSelection => await this.createTeamSelectionViewModelAsync(),
                ViewType.UserProfileSelection => await this.createUserProfileSelectionViewModelAsync(),
                _ => throw new ArgumentOutOfRangeException(nameof(viewType))
            };
        }
    }
}