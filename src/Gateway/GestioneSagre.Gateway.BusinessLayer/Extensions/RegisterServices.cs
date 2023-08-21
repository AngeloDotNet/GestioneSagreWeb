using GestioneSagre.GenericServices.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GestioneSagre.Gateway.BusinessLayer.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddRegisterConfigureServices(this IServiceCollection services, IConfiguration configuration, string serviceName, string swaggerName)
    {
        var customServices = configuration.GetSection("PointAPI");

        services.AddPolicyCors(serviceName);
        services.AddSerilogSeqServices();

        services.AddSwaggerGenConfig($"{swaggerName}", "v1");
        services.AddHealthChecks()
            .AddProcessAllocatedMemoryHealthCheck(100, name: "Allocated Memory")
            .AddUrlGroup(new Uri(customServices["Url"]), customServices["Name"], HealthStatus.Degraded, new[] { "web api", "swagger" });

        return services;
    }
}