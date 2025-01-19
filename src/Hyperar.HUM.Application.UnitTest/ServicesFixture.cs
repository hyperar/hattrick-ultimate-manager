namespace Hyperar.HUM.Application.UnitTest
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
            services.AddScoped<ImagePersister>();
            services.AddScoped<ManagerCompendiumPersister>();
            services.AddScoped<WorldDetailsPersister>();

            this.Services = services.BuildServiceProvider();
        }

        public IServiceProvider Services { get; private set; }
    }
}