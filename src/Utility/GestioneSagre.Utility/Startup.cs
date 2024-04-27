namespace GestioneSagre.Utility;

public class Startup
{
    private readonly string serviceName = "GestioneSagre.Utility";
    private readonly string swaggerName = "Gestione Sagre Utility";

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
        UpdateDatabase(app);

        app.AddCustomConfigure(swaggerName, serviceName);
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks();
        });
    }

    private static void UpdateDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetRequiredService<UtilityDbContext>();

        DBSeeder.Seed(context);
    }
}