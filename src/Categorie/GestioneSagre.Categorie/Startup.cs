using GestioneSagre.Categorie.BusinessLayer.Extensions;
using GestioneSagre.Categorie.MessageBroker.Extensions;
using GestioneSagre.GenericServices.Extensions;
using GestioneSagre.Shared.Extensions;

namespace GestioneSagre.Categorie;

public class Startup
{
    private readonly string serviceName = "GestioneSagre.Categorie";
    private readonly string swaggerName = "Gestione Sagre Categorie";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddRegisterConfigureServices(Configuration, serviceName, swaggerName);

        services.AddDefaultOperationResult();
        services.AddCustomProblemDetails(Configuration);

        services.AddServiceMessageBroker(Configuration);
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.AddCustomConfigure(swaggerName, serviceName);
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks();
        });
    }
}