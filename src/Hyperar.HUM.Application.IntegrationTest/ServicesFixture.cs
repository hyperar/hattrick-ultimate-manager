namespace Hyperar.HUM.Application.IntegrationTest
{
    using System;
    using System.Reflection;
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

        private const string RequestTokenKeyName = "OAuth:Endpoints:Base:RequestToken";

        private const string UserAgentKeyName = "OAuth:UserAgent";

        public ServicesFixture()
        {
            var services = new ServiceCollection();

            // Database.
            void configureDbContext(DbContextOptionsBuilder o)
            {
                //o.UseLazyLoadingProxies();
                o.UseInMemoryDatabase("HUMDB");
            }

            services.AddSingleton<IConfiguration>((services) => new ConfigurationBuilder().AddJsonFile("appSettings.json").Build());

            services.AddMediatR((config) =>
            {
                config.Lifetime = ServiceLifetime.Scoped;

                config.RegisterServicesFromAssembly(
                    Assembly.Load("Hyperar.HUM.Application"));
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IHattrickRepository<>), typeof(HattrickRepository<>));
            services.AddDbContext<IDatabaseContext, DatabaseContext>(configureDbContext, ServiceLifetime.Scoped);

            // Services.
            services.AddScoped<IFileDownloadTaskFactory, FileDownloadTaskFactory>();
            services.AddSingleton<IProtectedResourceUrlFactory, ProtectedResourceUrlFactory>();

            // Hattrick API client.
            services.AddSingleton<IHattrickService, HattrickService>((services) =>
            {
                var configuration = services.GetRequiredService<IConfiguration>();

                var wireMockServer = WireMockServerFactory.GetServer();

                var accessTokenUrlMask = configuration[AccessTokenKeyName];
                var authorizationUrlMask = configuration[AuthorizeKeyName];
                var callbackUrlMask = configuration[CallbackKeyName];
                var checkTokenUrlMask = configuration[CheckTokenKeyName];
                var invalidateTokenUrlMask = configuration[InvalidateTokenKeyName];
                var requestTokenUrlMask = configuration[RequestTokenKeyName];

                var consumerKey = configuration[ConsumerKeyKeyName];
                var consumerSecret = configuration[ConsumerSecretKeyName];
                var userAgent = configuration[UserAgentKeyName];

                ArgumentException.ThrowIfNullOrWhiteSpace(accessTokenUrlMask);
                ArgumentException.ThrowIfNullOrWhiteSpace(authorizationUrlMask);
                ArgumentException.ThrowIfNullOrWhiteSpace(callbackUrlMask);
                ArgumentException.ThrowIfNullOrWhiteSpace(checkTokenUrlMask);
                ArgumentException.ThrowIfNullOrWhiteSpace(invalidateTokenUrlMask);
                ArgumentException.ThrowIfNullOrWhiteSpace(requestTokenUrlMask);
                ArgumentException.ThrowIfNullOrWhiteSpace(consumerKey);
                ArgumentException.ThrowIfNullOrWhiteSpace(consumerSecret);
                ArgumentException.ThrowIfNullOrWhiteSpace(userAgent);

                var hattrickService = new HattrickService(
                    string.Format(accessTokenUrlMask, wireMockServer.Port),
                    string.Format(authorizationUrlMask, wireMockServer.Port),
                    string.Format(callbackUrlMask, wireMockServer.Port),
                    string.Format(checkTokenUrlMask, wireMockServer.Port),
                    string.Format(invalidateTokenUrlMask, wireMockServer.Port),
                    string.Format(requestTokenUrlMask, wireMockServer.Port),
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
            services.AddScoped<IFileParseStrategyFactory, FileParseStrategyFactory>();
            services.AddScoped<CheckTokenParser>();
            services.AddScoped<ErrorParser>();
            services.AddScoped<ManagerCompendiumParser>();
            services.AddScoped<WorldDetailsParser>();

            // Extractors.
            services.AddScoped<IFileExtractStrategyFactory, FileExtractStrategyFactory>();
            services.AddScoped<CheckTokenExtractor>();
            services.AddScoped<EmptyExtractor>();
            services.AddScoped<ManagerCompendiumExtractor>();
            services.AddScoped<WorldDetailsExtractor>();

            // Persisters.
            services.AddScoped<IFilePersisterStrategyFactory, FilePersisterStrategyFactory>();
            services.AddScoped<EmptyPersister>();
            services.AddScoped<ManagerCompendiumPersister>();
            services.AddScoped<WorldDetailsPersister>();

            var wireMockServer = WireMockServerFactory.GetServer();

            this.Services = services.BuildServiceProvider();
        }

        public IServiceProvider Services { get; private set; }
    }
}
