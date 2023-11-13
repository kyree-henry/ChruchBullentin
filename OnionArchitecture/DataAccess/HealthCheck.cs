using ChruchBulletin.DataAccess.Mapping;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace ChruchBulletin.DataAccess
{
    public class HealthCheck : IHealthCheck
    {
        private readonly ILogger<HealthCheck> _logger;
        private readonly DataContext _context;
        public HealthCheck(ILogger<HealthCheck> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (!_context.Database.CanConnect())
                return Task.FromResult(HealthCheckResult.Unhealthy("Cannot connect to database"));

            _logger.LogInformation($"Health check success");
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
