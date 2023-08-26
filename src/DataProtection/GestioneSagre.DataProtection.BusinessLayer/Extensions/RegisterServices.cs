using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GestioneSagre.DataProtection.BusinessLayer.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddRegisterConfigureServices(this IServiceCollection services, IConfiguration configuration, string serviceName, string swaggerName)
    {
        var customServices = configuration.GetSection("PointAPI");
        var connectionSystem = configuration.GetSection("ConnectionStrings");

        var connectionString = string.Empty;

        if (connectionSystem["TypeStartup"] == "Default")
        {
            connectionString = connectionSystem["Default"];
        }
        else
        {
            connectionString = connectionSystem["Docker"];
        }

        services.AddPolicyCors(serviceName);
        services.AddSerilogSeqServices();

        services.AddSwaggerGenConfig($"{swaggerName}", "v1");
        services.AddHealthChecks()
            .AddProcessAllocatedMemoryHealthCheck(100, name: "Allocated Memory")
            .AddUrlGroup(new Uri(customServices["Url"]), customServices["Name"], HealthStatus.Degraded, new[] { "web api", "swagger" })
            .AddSqlServer(connectionString, null, "DataBase Service", HealthStatus.Degraded, new[] { "database", "sqlserver" });

        services.AddDbContextSQLServer<ApplicationDbContext>(connectionString);
        services.AddTransient<IDataProtectionService, DataProtectionService>();

        services.AddDataProtection()
           .SetApplicationName("GestioneSagre")
           .SetDefaultKeyLifetime(TimeSpan.FromDays(14))
           .PersistKeysToDbContext<ApplicationDbContext>();

        return services;
    }
}