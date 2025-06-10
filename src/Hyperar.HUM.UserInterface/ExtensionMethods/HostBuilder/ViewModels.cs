namespace Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using Hyperar.HUM.UserInterface.ViewModels;
    using Hyperar.HUM.UserInterface.ViewModels.Interfaces;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    internal static class ViewModels
    {
        internal static IHostBuilder RegisterViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();

                services.AddTransient<CreateViewModelAsync<AuthorizationViewModel>>(services => () => CreateAuthorizationViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<DownloadViewModel>>(services => () => CreateDownloadViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<HomeViewModel>>(services => () => CreateHomeViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<TeamSelectionViewModel>>(services => () => CreateTeamSelectionViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<UserProfileSelectionViewModel>>(services => () => CreateUserProfileSelectionViewModelAsync(services));
            });

            return host;
        }

        private static async Task<AuthorizationViewModel> CreateAuthorizationViewModelAsync(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new AuthorizationViewModel(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<ISessionStore>(),
                scope.ServiceProvider.GetRequiredService<ISender>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<DownloadViewModel> CreateDownloadViewModelAsync(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new DownloadViewModel(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<ISessionStore>(),
                scope.ServiceProvider.GetRequiredService<ISender>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<HomeViewModel> CreateHomeViewModelAsync(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new HomeViewModel(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<ISessionStore>(),
                scope.ServiceProvider.GetRequiredService<ISender>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<TeamSelectionViewModel> CreateTeamSelectionViewModelAsync(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new TeamSelectionViewModel(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<ISessionStore>(),
                scope.ServiceProvider.GetRequiredService<ISender>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<UserProfileSelectionViewModel> CreateUserProfileSelectionViewModelAsync(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new UserProfileSelectionViewModel(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<ISessionStore>(),
                scope.ServiceProvider.GetRequiredService<ISender>());

            await viewModel.InitializeAsync();

            return viewModel;
        }
    }
}