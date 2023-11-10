using ChruchBulletin.DataAccess.Handlers;
using ChruchBulletin.DataAccess.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChruchBulletin.DataAccess.Test
{
    public static class TestHost
    {
        private static bool _dependenciesRegistered;
        private static readonly object Lock = new();
        private static IHost? _host;

        public static IHost Instance
        {
            get
            {
                EnsureDependenciesRegistered();
                return _host!;
            }
        }

        public static T GetRequiredService<T>() where T : notnull
        {
            IServiceScope serviceScope = Instance.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            return provider.GetRequiredService<T>();
        }

        private static void Initialize()
        {
            IHost host = Host.CreateDefaultBuilder()
                .UseEnvironment("Development")
                .ConfigureAppConfiguration((context, config) =>
                {
                    IHostEnvironment env = context.HostingEnvironment;

                    config
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureServices(s =>
                {
                    s.AddTransient<IDatabaseConfiguration, TestDatabaseConfiguration>();
                    s.AddTransient<BulletinItemByDateHandler, BulletinItemByDateHandler>();
                    s.AddTransient<BulletinItemByDateHandler>();
                    s.AddScoped<DbContext, DataContext>();
                    s.AddDbContextFactory<DataContext>();
                    s.AddDbContextFactory<DbContext>();
                })
                .Build();


            _host = host;
        }

        private static void EnsureDependenciesRegistered()
        {
            if (!_dependenciesRegistered)
                lock (Lock)
                {
                    if (!_dependenciesRegistered)
                    {
                        Initialize();
                        _dependenciesRegistered = true;
                    }
                }
        }
    }
}
