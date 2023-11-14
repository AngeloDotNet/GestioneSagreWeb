namespace GestioneSagre.Aggregator.Shared.Extensions;

public static class ConfigurationExtensions
{
    public static HealthCheckOptions GetHealthCheckOptions(this IConfiguration configuration)
    {
        var options = new HealthCheckOptions();
        configuration.GetSection("HealthCheck").Bind(options);
        return options;
    }

    public static List<HealthCheckServiceOptions> GetHealthCheckServices(this IConfiguration configuration)
    {
        var healthCheckServices = new List<HealthCheckServiceOptions>();
        configuration.GetSection("Services").Bind(healthCheckServices);

        return healthCheckServices.OrderBy(o => o.Name).ToList();
    }
}