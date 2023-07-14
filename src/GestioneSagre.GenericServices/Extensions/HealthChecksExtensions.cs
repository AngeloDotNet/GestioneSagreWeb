using static GestioneSagre.GenericServices.Models.HealthChecksToDatabaseExtensions;
using static GestioneSagre.GenericServices.Models.HealthChecksToWebExtensions;

namespace GestioneSagre.GenericServices.Extensions;

public static partial class HealthChecksExtensions
{
    public static WebApplication UseHealthChecksUI(this WebApplication app)
    {
        app.UseHealthChecks("/healthz", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
            },
        }).UseHealthChecksUI(options =>
        {
            options.ApiPath = "/healthcheck";
            options.UIPath = "/healthcheck-ui";
            options.PageTitle = "Health Check";
            options.AsideMenuOpened = true;
        });

        return app;
    }

    public static IServiceCollection AddHealthChecksUICentralized(this IServiceCollection services, string nameAPI,
        List<HealthChecksWebExtensions> listSwaggerToCheck, List<HealthChecksDatabaseExtensions> listDatabaseToCheck, int pollingInterval)
    {
        services.AddHealthChecks()
            .AddCheck<MemoryHealthCheck>(name: "Health Check Memory", tags: new[] { "memory" }, failureStatus: HealthStatus.Degraded)
            .AddingUrlGroup(listSwaggerToCheck)
            //.AddRabbitMQ(rabbitConnection, name: "Health Check RabbitMQ", failureStatus: HealthStatus.Degraded, timeout: TimeSpan.FromSeconds(5))
            .AddingSQLServerGroup(listDatabaseToCheck);

        services.AddHealthChecksUI(setupSettings: options =>
        {
            options.AddHealthCheckEndpoint($"Health Check {nameAPI}", $"/healthz");
            options.SetEvaluationTimeInSeconds(pollingInterval);
            //options.AddWebhookNotification("email",
            //    uri: "http://localhost:5008/api/noti/email",
            //    payload: "{ \"message\": \"Webhook report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\"}",
            //    restorePayload: "{ \"message\": \"[[LIVENESS]] is back to life\"}");
        }).AddInMemoryStorage();

        return services;
    }

    private static IHealthChecksBuilder AddingUrlGroup(this IHealthChecksBuilder checksBuilder,
        List<HealthChecksWebExtensions> webPathToCheck)
    {
        List<Tuple<string, string, string, string>> values = new();

        foreach (var webPath in webPathToCheck)
        {
            checksBuilder.AddUrlGroup(new Uri(webPath.UriWeb),
                name: webPath.NameGroup,
                failureStatus: HealthStatus.Degraded,
                tags: webPath.TagsGroup,
                timeout: TimeSpan.FromSeconds(webPath.PollingInterval));
        }

        return checksBuilder;
    }

    private static IHealthChecksBuilder AddingSQLServerGroup(this IHealthChecksBuilder checksBuilder,
        List<HealthChecksDatabaseExtensions> dbToChecks)
    {
        foreach (var dbPath in dbToChecks)
        {
            checksBuilder.AddSqlServer(dbPath.DbConnection,
                name: dbPath.NameGroup,
                failureStatus: HealthStatus.Degraded,
                tags: dbPath.TagsGroup,
                timeout: TimeSpan.FromSeconds(dbPath.PollingInterval));
        }

        return checksBuilder;
    }
}