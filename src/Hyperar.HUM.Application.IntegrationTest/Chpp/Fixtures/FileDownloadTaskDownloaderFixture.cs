namespace Hyperar.HUM.Application.IntegrationTest.Chpp.Fixtures
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

    public class FileDownloadTaskDownloaderFixture
    {
        public FileDownloadTaskDownloaderFixture()
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
            services.AddSingleton<IHattrickService, HattrickService>();

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

            this.Services = services.BuildServiceProvider();
        }

        public IServiceProvider Services { get; private set; }
    }
}
