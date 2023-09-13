using GestioneSagre.Aggregator.Shared.Extensions;
using GestioneSagre.GenericServices.Extensions;

namespace GestioneSagre.Aggregator;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = string.Empty;
        var connectionSystem = Configuration.GetSection("ConnectionStrings");

        if (connectionSystem["TypeStartup"] == "Default")
        {
            connectionString = connectionSystem["Default"];
        }
        else
        {
            connectionString = connectionSystem["Docker"];
        }

        services.AddSerilogSeqServices();
        services.AddHealthChecksUI(settings =>
        {
            settings.SetHeaderText(Configuration.GetHealthCheckOptions().HeaderText);
            settings.SetEvaluationTimeInSeconds(Configuration.GetHealthCheckOptions().PollingInterval);
            //settings.AddWebhookNotification("email",
            //    uri: "http://localhost:5008/api/noti/email",
            //    payload: "{ \"message\": \"Webhook report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\"}",
            //    restorePayload: "{ \"message\": \"[[LIVENESS]] is back to life\"}"
            //);
            //settings.SetMinimumSecondsBetweenFailureNotifications(120);
            settings.MaximumHistoryEntriesPerEndpoint(100);

            Configuration.GetHealthCheckServices().ForEach(service =>
            {
                settings.AddHealthCheckEndpoint(service.Name, service.Url);
            });
        }).AddSqlServerStorage(connectionString);
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.UseRouting();
        app.AddSerilogConfigureServices();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecksUI(setup =>
            {
                setup.ApiPath = Configuration.GetHealthCheckOptions().ApiPath;
                setup.UIPath = Configuration.GetHealthCheckOptions().UIPath;
                setup.AsideMenuOpened = true;
                setup.PageTitle = "Gestione Sagre Monitoring";
                setup.AddCustomStylesheet("dotnet.css");
            });
        });
    }
}