namespace Hyperar.HUM.UserInterface.ExtensionMethods.HostBuilder
{
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Infrastructure.Database;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    internal static class Database
    {
        internal static IHostBuilder RegisterDatabaseObjects(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("LocalDb");

                void configureDbContext(DbContextOptionsBuilder o)
                {
                    o.UseLazyLoadingProxies();
                    o.UseSqlServer(connectionString);
                }

                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                services.AddScoped(typeof(IHattrickRepository<>), typeof(HattrickRepository<>));
                services.AddDbContext<IDatabaseContext, DatabaseContext>(configureDbContext, ServiceLifetime.Scoped);
            });

            return host;
        }
    }
}