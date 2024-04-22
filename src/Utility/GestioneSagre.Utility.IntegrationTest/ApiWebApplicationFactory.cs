namespace GestioneSagre.Utility.IntegrationTest;

public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d
                => d.ServiceType == typeof(DbContextOptions<UtilityDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<UtilityDbContext>(options
                => options.UseInMemoryDatabase("Utility-InMemory-Integration-Test"));
        });
    }
}