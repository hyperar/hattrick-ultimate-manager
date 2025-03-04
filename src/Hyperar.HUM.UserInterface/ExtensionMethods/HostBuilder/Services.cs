namespace Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Factories;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Download;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist;
    using Hyperar.HUM.ChppApiClient;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    internal static class Services
    {
        private const string AccessTokenKeyName = "OAuth:Endpoints:Base:AccessToken";

        private const string AuthorizeKeyName = "OAuth:Endpoints:Base:Authorize";

        private const string CallbackKeyName = "OAuth:Endpoints:Base:Callback";

        private const string CheckTokenKeyName = "OAuth:Endpoints:Base:CheckToken";

        private const string ConsumerKeyKeyName = "OAuth:ConsumerKey";

        private const string ConsumerSecretKeyName = "OAuth:ConsumerSecret";

        private const string InvalidateTokenKeyName = "OAuth:Endpoints:Base:InvalidateToken";

        private const string ProtectedResourcesKeyName = "OAuth:Endpoints:Base:ProtectedResources";

        private const string RequestTokenKeyName = "OAuth:Endpoints:Base:RequestToken";

        private const string UserAgentKeyName = "OAuth:UserAgent";

        internal static IHostBuilder RegisterServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IProtectedResourceUrlFactory>((services) =>
                {
                    var configuration = services.GetRequiredService<IConfiguration>();

                    var protectedResourcesUrl = configuration[ProtectedResourcesKeyName];

                    ArgumentException.ThrowIfNullOrWhiteSpace(protectedResourcesUrl);

                    return new ProtectedResourceUrlFactory(configuration, protectedResourcesUrl);
                });

                services.AddSingleton<IHattrickService, HattrickService>((services) =>
                {
                    var configuration = services.GetRequiredService<IConfiguration>();

                    var accessTokenUrl = configuration[AccessTokenKeyName];
                    var authorizationUrl = configuration[AuthorizeKeyName];
                    var callbackUrl = configuration[CallbackKeyName];
                    var checkTokenUrl = configuration[CheckTokenKeyName];
                    var invalidateTokenUrl = configuration[InvalidateTokenKeyName];
                    var requestTokenUrl = configuration[RequestTokenKeyName];
                    var consumerKey = configuration[ConsumerKeyKeyName];
                    var consumerSecret = configuration[ConsumerSecretKeyName];
                    var userAgent = configuration[UserAgentKeyName];

                    ArgumentException.ThrowIfNullOrWhiteSpace(accessTokenUrl);
                    ArgumentException.ThrowIfNullOrWhiteSpace(authorizationUrl);
                    ArgumentException.ThrowIfNullOrWhiteSpace(callbackUrl);
                    ArgumentException.ThrowIfNullOrWhiteSpace(checkTokenUrl);
                    ArgumentException.ThrowIfNullOrWhiteSpace(invalidateTokenUrl);
                    ArgumentException.ThrowIfNullOrWhiteSpace(requestTokenUrl);
                    ArgumentException.ThrowIfNullOrWhiteSpace(consumerKey);
                    ArgumentException.ThrowIfNullOrWhiteSpace(consumerSecret);
                    ArgumentException.ThrowIfNullOrWhiteSpace(userAgent);

                    var hattrickService = new HattrickService(
                        accessTokenUrl,
                        authorizationUrl,
                        callbackUrl,
                        checkTokenUrl,
                        invalidateTokenUrl,
                        requestTokenUrl,
                        consumerKey,
                        consumerSecret,
                        userAgent,
                        services.GetRequiredService<IProtectedResourceUrlFactory>());

                    return hattrickService;
                });

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
                services.AddScoped<ImagePersister>();
                services.AddScoped<ManagerCompendiumPersister>();
                services.AddScoped<WorldDetailsPersister>();
            });

            return host;
        }
    }
}