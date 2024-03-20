using Prometheus;

namespace GestioneSagre.Gateway;

public class Startup
{
    private readonly string serviceName = "GestioneSagre.Gateway";
    private readonly string swaggerName = "Gestione Sagre Gateway";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddRegisterConfigureServices(Configuration, serviceName, swaggerName);

        services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        services.AddOcelot(Configuration);
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.UseSwaggerUINoRoutePrefix($"{swaggerName} v1");
        app.UseRouting();

        app.UseHttpMetrics(options =>
        {
            options.AddCustomLabel("host", context => context.Request.Host.Host);
        });

        app.UseCors($"{serviceName}");
        app.AddSerilogConfigureServices();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks();
        });

        app.UseOcelot().Wait();
    }
}