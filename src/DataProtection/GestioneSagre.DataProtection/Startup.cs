using GestioneSagre.DataProtection.BusinessLayer.Extensions;
using GestioneSagre.GenericServices.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace GestioneSagre.DataProtection;

public class Startup
{
    private readonly string serviceName = "GestioneSagre.DataProtection";
    private readonly string swaggerName = "Gestione Sagre Data Protection";

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
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.UseSwaggerUINoRoutePrefix($"{swaggerName} v1");
        app.UseRouting();

        app.UseCors($"{serviceName}");
        app.AddSerilogConfigureServices();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks();
        });
    }
}