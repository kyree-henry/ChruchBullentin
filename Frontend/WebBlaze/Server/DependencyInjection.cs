using ChruchBulletin.DataAccess.Mapping;
using Lamar;
using Microsoft.EntityFrameworkCore;

namespace ChruchBulletin.WebBlaze.Server
{
    public class DependencyInjection : ServiceRegistry
    {
        public DependencyInjection() 
        {
            this.AddScoped<DbContext, DataContext>();
            this.AddDbContextFactory<DataContext>();
            this.AddDbContextFactory<DbContext>();

            Scan(scanner =>
            {
                scanner.WithDefaultConventions();
                scanner.AssemblyContainingType<Core.HealthCheck>();
                scanner.AssemblyContainingType<API.HealthCheck>();
                scanner.AssemblyContainingType<DataAccess.HealthCheck>();
                scanner.AssemblyContainingType<HealthCheck>();

                this.AddHealthChecks()
                    .AddCheck<Core.HealthCheck>("Core")
                    .AddCheck<API.HealthCheck>("API")
                    .AddCheck<HealthCheck>("Server")
                    .AddCheck<DataAccess.HealthCheck>("DataAccess");
            });
        }
    }
}
