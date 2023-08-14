using GestioneSagre.GenericServices.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static GestioneSagre.GenericServices.Models.HealthChecksToDatabaseExtensions;
using static GestioneSagre.GenericServices.Models.HealthChecksToWebExtensions;

namespace GestioneSagre.Gateway.BusinessLayer.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddRegisterConfigureServices(this IServiceCollection services, IConfiguration configuration, string serviceName, string swaggerName)
    {
        var envDev = configuration.GetSection("EnvironmentDev");
        var envProd = configuration.GetSection("EnvironmentProd");

        var tagsSwagger = new[] { "webapi", "swagger" };
        var tagsDatabase = new[] { "database", "sqlserver" };

        var listSwagger = new List<HealthChecksWebExtensions>();
        var listDatabase = new List<HealthChecksDatabaseExtensions>();

        var pollingSwagger = 15;
        var pollingDatabase = 30;

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            listSwagger = new List<HealthChecksWebExtensions>()
            {
                new HealthChecksWebExtensions($"http://{envDev["Swagger-Hostname"]}:5179/swagger/index.html", "WebApi Gateway", tagsSwagger, pollingSwagger),
                //new HealthChecksWebExtensions(envDev["Swagger-DataProtection"], "WebApi Data Protection", tagsSwagger, pollingSwagger)
            };

            listDatabase = new List<HealthChecksDatabaseExtensions>()
            {
                new HealthChecksDatabaseExtensions(envDev["SQLServer-DataProtection"], "SQL Server Data Protection", tagsDatabase, pollingDatabase),
            //    new HealthChecksDatabaseExtensions(envDev["SQLServer-Utility"], "SQL Server Utility", tagsDatabase, pollingDatabase)
            };
        }
        else
        {
            listSwagger = new List<HealthChecksWebExtensions>()
            {
                new HealthChecksWebExtensions($"http://{envProd["Swagger-Hostname"]}:5001/swagger/index.html", "WebApi Gateway", tagsSwagger, pollingSwagger),
                //new HealthChecksWebExtensions(envProd["Swagger-DataProtection"], "WebApi Data Protection", tagsSwagger, pollingSwagger)
            };

            listDatabase = new List<HealthChecksDatabaseExtensions>()
            {
                new HealthChecksDatabaseExtensions(envProd["SQLServer-DataProtection"], "SQL Server Data Protection", tagsDatabase, pollingDatabase),
                //new HealthChecksDatabaseExtensions(envProd["SQLServer-Utility"], "SQL Server Utility", tagsDatabase, pollingDatabase)
            };
        }

        services.AddPolicyCors(serviceName);
        services.AddSerilogSeqServices();

        services.AddSwaggerGenConfig($"{swaggerName}", "v1");
        services.AddHealthChecksUICentralized("Gestione Sagre", listSwagger, listDatabase, 15);

        return services;
    }
}