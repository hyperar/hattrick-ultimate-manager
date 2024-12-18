namespace Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.Controllers;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.ViewModels;
    using Hyperar.HUM.UserInterface.ViewModels.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    internal static class ViewModels
    {
        internal static IHostBuilder RegisterViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();

                services.AddTransient<CreateViewModelAsync<DownloadViewModel>>(services => () => CreateDownloadViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<HomeViewModel>>(services => () => CreateHomeViewModelAsync(services));
                services.AddSingleton<CreateViewModelAsync<MainWindowViewModel>>(services => () => CreateMainWindowViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<UserProfileViewModel>>(services => () => CreateUserProfileViewModelAsync(services));
            });

            return host;
        }

        private static async Task<DownloadViewModel> CreateDownloadViewModelAsync(IServiceProvider services)
        {
            DownloadViewModel viewModel = new DownloadViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<HomeViewModel> CreateHomeViewModelAsync(IServiceProvider services)
        {
            HomeViewModel viewModel = new HomeViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<MainWindowViewModel> CreateMainWindowViewModelAsync(IServiceProvider services)
        {
            var scope = services.CreateScope();

            MainWindowViewModel viewModel = new MainWindowViewModel(
                services.GetRequiredService<INavigator>(),
                scope.ServiceProvider.GetRequiredService<IMainMenuBuilderFactory>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<UserProfileViewModel> CreateUserProfileViewModelAsync(IServiceProvider services)
        {
            UserProfileViewModel viewModel = new UserProfileViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }
    }
}