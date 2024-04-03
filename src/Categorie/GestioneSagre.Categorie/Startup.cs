using GestioneSagre.Categorie.BusinessLayer.Extensions;
using GestioneSagre.GenericServices.Extensions;
using GestioneSagre.Messaging.Extensions;
using GestioneSagre.Messaging.Models.InputModels;
using GestioneSagre.Messaging.Models.Options;
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

        //services.AddServiceMessageBroker(Configuration);
        //var rabbitOptions = Configuration.GetSection("RabbitMQ").Get<RabbitProducerOptions>();
        //var rabbitConfig = new RabbitOptions()
        //{
        //    Host = rabbitOptions.Host,
        //    VirtualHost = rabbitOptions.VirtualHost,
        //    Username = rabbitOptions.Username,
        //    Password = rabbitOptions.Password,
        //    ReceivedEndpoint = rabbitOptions.ReceivedEndpoint,
        //    //Durable = rabbitOptions.Durable,
        //    //AutoDelete = rabbitOptions.AutoDelete,
        //    //ExchangeType = rabbitOptions.ExchangeType,
        //    //PrefetchCount = rabbitOptions.PrefetchCount,
        //    //RetryCount = rabbitOptions.RetryCount,
        //    //RetryInterval = rabbitOptions.RetryInterval,
        //    //QueueExpiration = rabbitOptions.QueueExpiration
        //};
        //services.AddRabbitMQSender<RequestFestaAttiva>(rabbitConfig);

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