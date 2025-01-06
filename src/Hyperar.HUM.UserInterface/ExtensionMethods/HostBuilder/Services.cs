namespace Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder
{
    using Hyperar.HUM.Application.ChppFile.Download.Command;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Factories;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Download;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist;
    using Hyperar.HUM.ChppApiClient;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    internal static class Services
    {
        internal static IHostBuilder RegisterServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IProtectedResourceUrlFactory, ProtectedResourceUrlFactory>();
                services.AddSingleton<IHattrickService, HattrickService>();

                // Factories.
                services.AddScoped<IFileDownloadTaskFactory, FileDownloadTaskFactory>();
                services.AddScoped<IFileDownloadTaskStepFactory, FileDownloadTaskStepFactory>();
                services.AddScoped<IFileParseStrategyFactory, FileParseStrategyFactory>();
                services.AddScoped<IFileExtractStrategyFactory, FileExtractStrategyFactory>();
                services.AddScoped<IFilePersisterStrategyFactory, FilePersisterStrategyFactory>();

                // Steps.
                services.AddScoped<IFileDownloadTaskDownloader, FileDownloadTaskDownloader>();
                services.AddScoped<IFileDownloadTaskExtractor, FileDownloadTaskExtractor>();
                services.AddScoped<IFileDownloadTaskParser, FileDownloadTaskParser>();
                services.AddScoped<IFileDownloadTaskPersister, FileDownloadTaskPersister>();

                // Downloaders.
                services.AddScoped<CheckTokenDownloader>();
                services.AddScoped<ImageFileDownloader>();
                services.AddScoped<ProtectedResourceDownloader>();

                // Parsers.
                services.AddScoped<CheckTokenParser>();
                services.AddScoped<ErrorParser>();
                services.AddScoped<ManagerCompendiumParser>();
                services.AddScoped<WorldDetailsParser>();

                // Extractors.
                services.AddScoped<CheckTokenExtractor>();
                services.AddScoped<EmptyExtractor>();
                services.AddScoped<ManagerCompendiumExtractor>();
                services.AddScoped<WorldDetailsExtractor>();

                // Persisters.
                services.AddScoped<EmptyPersister>();
                services.AddScoped<ManagerCompendiumPersister>();
                services.AddScoped<WorldDetailsPersister>();
            });

            return host;
        }
    }
}