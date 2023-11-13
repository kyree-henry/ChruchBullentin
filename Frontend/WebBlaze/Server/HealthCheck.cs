using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ChruchBulletin.WebBlaze.Server
{
    public class HealthCheck : IHealthCheck
    {
        private readonly ILogger<HealthCheck> _logger;

        public HealthCheck(ILogger<HealthCheck> logger)
        {
            _logger = logger;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            _logger.LogInformation($"Health check success");
            if (!Environment.Is64BitProcess)
            {
                return Task.FromResult(HealthCheckResult.Degraded());
            }

            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
