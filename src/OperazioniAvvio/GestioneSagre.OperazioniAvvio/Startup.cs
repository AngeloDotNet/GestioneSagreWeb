using GestioneSagre.GenericServices.Extensions;
using GestioneSagre.OperazioniAvvio.BusinessLayer.Extensions;
using GestioneSagre.Shared.Extensions;

namespace GestioneSagre.OperazioniAvvio;

public class Startup
{
    private readonly string serviceName = "GestioneSagre.OperazioniAvvio";
    private readonly string swaggerName = "Gestione Sagre Operazioni Avvio";

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