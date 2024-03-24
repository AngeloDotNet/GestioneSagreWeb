namespace GestioneSagre.Gateway.BusinessLayer.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddRegisterConfigureServices(this IServiceCollection services, IConfiguration configuration, string serviceName, string swaggerName)
    {
        var customServices = configuration.GetSection("PointAPI");
        var serverService = configuration.GetSection("Servers");

        var connStringRedis = serverService["Redis-Host"] + ":" + serverService["Redis-Port"];
        var connStringSeq = serverService["Seq-Host"];
        var connStringRabbitMQ = "amqp://" + serverService["RabbitMQ-Login"] + "@" + serverService["RabbitMQ-Host"];
        var SeqApiKey = serverService["Seq-ApiKey"];

        var tagsAPI = new[] { "web api", "swagger" };
        var tagsRedis = new string[] { "infrastructure", "redis" };
        var tagsRabbitMQ = new string[] { "infrastructure", "rabbitMQ" };

        services.AddPolicyCors(serviceName);
        services.AddSerilogSeqServices();

        services.AddSwaggerGenConfig($"{swaggerName}", "v1");
        services.AddHealthChecks()
            .AddRedis(connStringRedis, "Redis Check", HealthStatus.Degraded, tagsRedis)
            .AddRabbitMQ(connStringRabbitMQ, null, "RabbitMQ Check", HealthStatus.Degraded, tagsRabbitMQ, TimeSpan.FromSeconds(5));

        services.AddCustomHealthChecks(customServices["Url"], customServices["Name"], tagsAPI, connStringSeq, SeqApiKey);
        services.AddMetricServer(options => options.Url = "/metrics");

        return services;
    }
}