namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HUM.UserInterface.State.Enums;
    using Hyperar.HUM.UserInterface.ViewModels.Interfaces;

    internal delegate Task<TViewModel> CreateViewModelAsync<TViewModel>() where TViewModel : ViewModelBase;

    internal class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModelAsync<DownloadViewModel> createDownloadViewModelAsync;

        private readonly CreateViewModelAsync<HomeViewModel> createHomeViewModelAsync;

        private readonly CreateViewModelAsync<MainWindowViewModel> createMainWindowViewModelAsync;

        private readonly CreateViewModelAsync<UserProfileViewModel> createUserProfileViewModelAsync;

        public ViewModelFactory(
            CreateViewModelAsync<DownloadViewModel> createDownloadViewModelAsync,
            CreateViewModelAsync<HomeViewModel> createHomeViewModelAsync,
            CreateViewModelAsync<MainWindowViewModel> createMainWindowViewModelAsync,
            CreateViewModelAsync<UserProfileViewModel> createUserProfileViewModelAsync)
        {
            this.createDownloadViewModelAsync = createDownloadViewModelAsync;
            this.createHomeViewModelAsync = createHomeViewModelAsync;
            this.createMainWindowViewModelAsync = createMainWindowViewModelAsync;
            this.createUserProfileViewModelAsync = createUserProfileViewModelAsync;
        }

        public async Task<ViewModelBase> CreateViewModelAsync(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Home => await this.createHomeViewModelAsync(),
                ViewType.Main => await this.createMainWindowViewModelAsync(),
                ViewType.UserProfile => await this.createUserProfileViewModelAsync(),
                _ => throw new ArgumentOutOfRangeException(nameof(viewType))
            };
        }
    }
}