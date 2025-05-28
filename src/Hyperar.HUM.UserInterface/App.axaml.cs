namespace Hyperar.HUM.UserInterface
{
    using Avalonia;
    using Avalonia.Controls.ApplicationLifetimes;
    using Avalonia.Data.Core.Plugins;
    using Avalonia.Markup.Xaml;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using Hyperar.HUM.UserInterface.ViewModels;
    using Hyperar.HUM.UserInterface.ViewModels.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override async void OnFrameworkInitializationCompleted()
        {
            var host = Host.CreateDefaultBuilder()
                .RegisterConfiguration()
                .RegisterDatabaseObjects()
                .RegisterMediatR()
                .RegisterServices()
                .RegisterStateObjects()
                .RegisterStores()
                .RegisterViewModels()
                .Build();

            if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);

                var splashScreenWindow = new SplashScreenWindow();

                splashScreenWindow.Show();

                desktop.MainWindow = splashScreenWindow;

                using (var scope = host.Services.CreateScope())
                {
                    await scope.ServiceProvider.GetRequiredService<IDatabaseContext>()
                        .MigrateAsync();
                }

                var mainWindow = new MainWindow(host.Services.GetRequiredService<IConfiguration>());

                var landingViewFactory = host.Services.GetRequiredService<ILandingViewFactory>();

                var mainViewModel = new MainWindowViewModel(
                    host.Services.GetRequiredService<INavigator>(),
                    host.Services.GetRequiredService<ISessionStore>(),
                    host.Services.GetRequiredService<IViewModelFactory>(),
                    await landingViewFactory.GetLandingViewAsync());

                await mainViewModel.InitializeAsync();

                mainWindow.DataContext = mainViewModel;

                desktop.MainWindow = mainWindow;

                mainWindow.Show();

                splashScreenWindow.Close();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}