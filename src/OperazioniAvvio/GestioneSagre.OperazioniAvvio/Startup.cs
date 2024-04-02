using GestioneSagre.GenericServices.Extensions;
using GestioneSagre.Messaging.Extensions;
using GestioneSagre.Messaging.Models.Options;
using GestioneSagre.OperazioniAvvio.BusinessLayer.Extensions;
using GestioneSagre.OperazioniAvvio.MessageBroker.Consumers;
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

        //services.AddServiceMessageBroker(Configuration);
        var rabbitOptions = Configuration.GetSection("RabbitMQ").Get<RabbitConsumerOptions>();
        var rabbitConfig = new RabbitOptions()
        {
            Host = rabbitOptions.Host,
            VirtualHost = rabbitOptions.VirtualHost,
            Username = rabbitOptions.Username,
            Password = rabbitOptions.Password,
            ReceivedEndpoint = rabbitOptions.ReceivedEndpoint,
            //Durable = rabbitOptions.Durable,
            //AutoDelete = rabbitOptions.AutoDelete,
            //ExchangeType = rabbitOptions.ExchangeType,
            //PrefetchCount = rabbitOptions.PrefetchCount,
            //RetryCount = 0,
            //RetryInterval = 0,
            //QueueExpiration = 0
        };
        services.AddRabbitMQReceiver<ConsumerFestaAttiva>(rabbitConfig);
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