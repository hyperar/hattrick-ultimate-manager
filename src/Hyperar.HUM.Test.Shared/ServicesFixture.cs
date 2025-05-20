namespace Hyperar.HUM.Test.Shared
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
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Infrastructure.Database;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class ServicesFixture
    {
        private const string AccessTokenKeyName = "OAuth:Endpoints:Base:AccessToken";

        private const string AuthorizeKeyName = "OAuth:Endpoints:Base:Authorize";

        private const string CallbackKeyName = "OAuth:Endpoints:Base:Callback";

        private const string CheckTokenKeyName = "OAuth:Endpoints:Base:CheckToken";

        private const string ConsumerKeyKeyName = "OAuth:ConsumerKey";

        private const string ConsumerSecretKeyName = "OAuth:ConsumerSecret";

        private const string InvalidateTokenKeyName = "OAuth:Endpoints:Base:InvalidateToken";

        private const string ProtectedResourcesName = "OAuth:Endpoints:Base:ProtectedResources";

        private const string RequestTokenKeyName = "OAuth:Endpoints:Base:RequestToken";

        private const string UserAgentKeyName = "OAuth:UserAgent";

        public ServicesFixture()
        {
            var serverPort = WireMockServerFactory.StartServerAndGetPort();

            var services = new ServiceCollection();

            // Database.
            void configureDbContext(DbContextOptionsBuilder o)
            {
                o.UseInMemoryDatabase("HUMDB");
            }

            services.AddSingleton<IConfiguration>((services) => new ConfigurationBuilder()
                .AddJsonFile("appSettings.json").Build());

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IHattrickRepository<>), typeof(HattrickRepository<>));
            services.AddDbContext<IDatabaseContext, DatabaseContext>(configureDbContext, ServiceLifetime.Scoped);

            // Services.
            services.AddScoped<IFileDownloadTaskFactory, FileDownloadTaskFactory>();
            services.AddSingleton<IProtectedResourceUrlFactory>((services) =>
            {
                var configuration = services.GetRequiredService<IConfiguration>();

                var protectedResourcesUrlMask = configuration[ProtectedResourcesName];

                ArgumentException.ThrowIfNullOrWhiteSpace(protectedResourcesUrlMask);

                var protectedResourcesUrl = string.Format(protectedResourcesUrlMask, serverPort);

                return new ProtectedResourceUrlFactory(configuration, protectedResourcesUrl);
            });

            // Hattrick API client.
            services.AddSingleton<IHattrickService, HattrickService>((services) =>
            {
                var configuration = services.GetRequiredService<IConfiguration>();

                var accessTokenUrlMask = configuration[AccessTokenKeyName];
                var authorizationUrlMask = configuration[AuthorizeKeyName];
                var callbackUrl = configuration[CallbackKeyName];
                var checkTokenUrlMask = configuration[CheckTokenKeyName];
                var invalidateTokenUrlMask = configuration[InvalidateTokenKeyName];
                var requestTokenUrlMask = configuration[RequestTokenKeyName];
                var consumerKey = configuration[ConsumerKeyKeyName];
                var consumerSecret = configuration[ConsumerSecretKeyName];
                var userAgent = configuration[UserAgentKeyName];

                ArgumentException.ThrowIfNullOrWhiteSpace(accessTokenUrlMask);
                ArgumentException.ThrowIfNullOrWhiteSpace(authorizationUrlMask);
                ArgumentException.ThrowIfNullOrWhiteSpace(callbackUrl);
                ArgumentException.ThrowIfNullOrWhiteSpace(checkTokenUrlMask);
                ArgumentException.ThrowIfNullOrWhiteSpace(invalidateTokenUrlMask);
                ArgumentException.ThrowIfNullOrWhiteSpace(requestTokenUrlMask);
                ArgumentException.ThrowIfNullOrWhiteSpace(consumerKey);
                ArgumentException.ThrowIfNullOrWhiteSpace(consumerSecret);
                ArgumentException.ThrowIfNullOrWhiteSpace(userAgent);

                var accessTokenUrl = string.Format(accessTokenUrlMask, serverPort);
                var authorizationUrl = string.Format(authorizationUrlMask, serverPort);
                var checkTokenUrl = string.Format(checkTokenUrlMask, serverPort);
                var invalidateTokenUrl = string.Format(invalidateTokenUrlMask, serverPort);
                var requestTokenUrl = string.Format(requestTokenUrlMask, serverPort);

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

            // Steps.
            services.AddScoped<IFileDownloadTaskStepFactory, FileDownloadTaskStepFactory>();
            services.AddScoped<IFileDownloadTaskDownloader, FileDownloadTaskDownloader>();
            services.AddScoped<IFileDownloadTaskExtractor, FileDownloadTaskExtractor>();
            services.AddScoped<IFileDownloadTaskParser, FileDownloadTaskParser>();
            services.AddScoped<IFileDownloadTaskPersister, FileDownloadTaskPersister>();

            // Downloaders.
            services.AddScoped<CheckTokenDownloader>();
            services.AddScoped<ImageFileDownloader>();
            services.AddScoped<ProtectedResourceDownloader>();

            // Parsers.
            services.AddScoped<IXmlFileParser, XmlFileParser>();
            services.AddScoped<IFileParseStrategyFactory, FileParseStrategyFactory>();
            services.AddScoped<CheckTokenParser>();
            services.AddScoped<ErrorParser>();
            services.AddScoped<ManagerCompendiumParser>();
            services.AddScoped<TeamDetailsParser>();
            services.AddScoped<WorldDetailsParser>();

            // Extractors.
            services.AddScoped<IFileExtractStrategyFactory, FileExtractStrategyFactory>();
            services.AddScoped<CheckTokenExtractor>();
            services.AddScoped<EmptyExtractor>();
            services.AddScoped<ManagerCompendiumExtractor>();
            services.AddScoped<TeamDetailsExtractor>();
            services.AddScoped<WorldDetailsExtractor>();

            // Persisters.
            services.AddScoped<IFilePersisterStrategyFactory, FilePersisterStrategyFactory>();
            services.AddScoped<EmptyPersister>();
            services.AddScoped<ImagePersister>();
            services.AddScoped<ManagerCompendiumPersister>();
            services.AddScoped<TeamDetailsPersister>();
            services.AddScoped<WorldDetailsPersister>();

            this.Services = services.BuildServiceProvider();
        }

        public IServiceProvider Services { get; private set; }
    }
}