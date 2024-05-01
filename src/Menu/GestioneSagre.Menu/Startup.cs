using GestioneSagre.GenericServices.Extensions;
using GestioneSagre.Menu.BusinessLayer.Extensions;
using GestioneSagre.Messaging.Extensions;
using GestioneSagre.Messaging.Models.InputModels;
using GestioneSagre.Messaging.Models.Options;
using GestioneSagre.Shared.Extensions;

namespace GestioneSagre.Menu;

public class Startup
{
    private readonly string serviceName = "GestioneSagre.Menu";
    private readonly string swaggerName = "Gestione Sagre Menu";

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

        var rabbitOptions = Configuration.GetSection("RabbitMQ").Get<RabbitOptions>();
        services.AddRabbitMQSender<RequestFestaAttiva>(rabbitOptions);
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