using GestioneSagre.DataProtection.BusinessLayer.Commands;
using GestioneSagre.DataProtection.DataAccessLayer;
using GestioneSagre.EFCore.Extensions;
using GestioneSagre.GenericServices.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GestioneSagre.DataProtection.BusinessLayer.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddRegisterConfigureServices(this IServiceCollection services, IConfiguration configuration, string serviceName, string swaggerName)
    {
        var customServices = configuration.GetSection("PointAPI");
        var connectionSystem = configuration.GetSection("ConnectionStrings");
        var serverService = configuration.GetSection("Servers");

        var connectionString = string.Empty;
        var connStringSeq = serverService["Seq-Host"];
        var SeqApiKey = serverService["Seq-ApiKey"];
        var tagsAPI = new[] { "web api", "swagger" };
        var tagsSQLServer = new[] { "database", "sqlserver" };

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
        services.AddHealthChecks().AddSqlServer(connectionString, null, "DataBase Service", HealthStatus.Degraded, tagsSQLServer);
        services.AddCustomHealthChecks(customServices["Url"], customServices["Name"], tagsAPI, connStringSeq, SeqApiKey);

        services.AddDbContextSQLServer<ApplicationDbContext>(connectionString);
        services.AddTransient<IDataProtectionService, DataProtectionService>();

        services.AddDataProtection()
           .SetApplicationName("GestioneSagre")
           .SetDefaultKeyLifetime(TimeSpan.FromDays(14))
           .PersistKeysToDbContext<ApplicationDbContext>();

        return services;
    }
}