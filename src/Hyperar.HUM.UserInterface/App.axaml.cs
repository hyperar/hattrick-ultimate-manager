namespace Hyperar.HUM.UserInterface
{
    using System.Threading.Tasks;
    using Avalonia;
    using Avalonia.Controls.ApplicationLifetimes;
    using Avalonia.Data.Core.Plugins;
    using Avalonia.Markup.Xaml;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder;
    using Hyperar.HUM.UserInterface.State.Enums;
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
            IHost host = Host.CreateDefaultBuilder()
                .RegisterConfiguration()
                .RegisterControllers()
                .RegisterDatabaseObjects()
                .RegisterMediatR()
                //.RegisterServices()
                .RegisterStateObjects()
                .RegisterViewModels()
                //.RegisterFileDownloadStepProcesses()
                .Build();

            if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);

                SplashScreenWindow splashScreenWindow = new SplashScreenWindow();

                splashScreenWindow.Show();

                desktop.MainWindow = splashScreenWindow;

                using (IServiceScope scope = host.Services.CreateScope())
                {
                    await scope.ServiceProvider.GetRequiredService<IDatabaseContext>()
                        .MigrateAsync();
                }

                MainWindow mainWindow = new MainWindow(host.Services.GetRequiredService<IConfiguration>());

                IViewModelFactory viewModelFactory = host.Services.GetRequiredService<IViewModelFactory>();

                mainWindow.DataContext = await viewModelFactory.CreateViewModelAsync(ViewType.Main);

                desktop.MainWindow = mainWindow;

                mainWindow.Show();

                splashScreenWindow.Close();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}