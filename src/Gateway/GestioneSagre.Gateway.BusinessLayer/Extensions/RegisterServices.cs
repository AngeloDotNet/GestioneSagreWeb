namespace GestioneSagre.Gateway.BusinessLayer.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddRegisterConfigureServices(this IServiceCollection services, IConfiguration configuration, string serviceName, string swaggerName)
    {
        var customServices = configuration.GetSection("PointAPI");
        var serverService = configuration.GetSection("Servers");

        var connStringRedis = serverService["Redis-Host"] + ":" + serverService["Redis-Port"];
        var connStringSeq = serverService["Seq-Host"];
        var SeqApiKey = serverService["Seq-ApiKey"];
        var tagsAPI = new[] { "web api", "swagger" };
        var tagsRedis = new string[] { "infrastructure", "redis" };

        services.AddPolicyCors(serviceName);
        services.AddSerilogSeqServices();

        services.AddSwaggerGenConfig($"{swaggerName}", "v1");
        services.AddHealthChecks().AddRedis(connStringRedis, "Redis Check", HealthStatus.Degraded, tagsRedis);
        services.AddCustomHealthChecks(customServices["Url"], customServices["Name"], tagsAPI, connStringSeq, SeqApiKey);

        return services;
    }
}