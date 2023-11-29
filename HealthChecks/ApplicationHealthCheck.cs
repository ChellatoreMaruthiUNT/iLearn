using iLearn.Data;
using iLearn.Services.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;

namespace iLearn.HealthChecks
{
    public class ApplicationHealthCheck
    {
        public class DatabaseHealthCheck : IHealthCheck
        {
            private readonly iLearnContext _dbConnection; // Replace IDbConnection with your specific database connection type

            public DatabaseHealthCheck(iLearnContext dbConnection)
            {
                _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            }

            public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
            {
                try
                {
                    var canConnect = _dbConnection.Database.CanConnect();

                    if (canConnect)
                    {
                        return Task.FromResult(HealthCheckResult.Healthy("Database connection is healthy."));
                    }
                    else
                    {
                        return Task.FromResult(HealthCheckResult.Unhealthy("Database connection is not open."));
                    }
                }
                catch (Exception ex)
                {
                    return Task.FromResult(HealthCheckResult.Unhealthy("Database connection check failed.", ex));
                }
            }
        }

        public class FileStorageHealthCheck : IHealthCheck
        {
            private readonly IFileStorageService _fileStorageService; // Replace IFileStorageService with your file storage service interface

            public FileStorageHealthCheck(IFileStorageService fileStorageService)
            {
                _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
            }

            public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
            {
                try
                {
                    var isHealthy = true;

                    if (isHealthy)
                    {
                        return HealthCheckResult.Healthy("File storage connection is healthy.");
                    }
                    else
                    {
                        return HealthCheckResult.Unhealthy("File storage connection is not healthy.");
                    }
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy("File storage connection check failed.", ex);
                }
            }
        }
    }
}
