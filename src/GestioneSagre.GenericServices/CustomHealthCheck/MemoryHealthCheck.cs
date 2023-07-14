﻿namespace GestioneSagre.GenericServices.CustomHealthCheck;

public class MemoryHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var allocated = GC.GetTotalMemory(forceFullCollection: false);
        var data = new Dictionary<string, object>()
        {
            { "AllocatedBytes", allocated },
            { "Gen0Collections", GC.CollectionCount(0) },
            { "Gen1Collections", GC.CollectionCount(1) },
            { "Gen2Collections", GC.CollectionCount(2) },
        };
        var threshold = 1024L * 1024L * 1024L;
        var status = allocated < threshold ? HealthStatus.Healthy : context.Registration.FailureStatus;

        return Task.FromResult(new HealthCheckResult(status,
            description: "Reports degraded status if allocated bytes " + $">= {threshold} bytes.", exception: null, data: data));
    }
}