namespace GestioneSagre.EFCore.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddDbContextSQLServer<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(optionBuilder =>
        {
            optionBuilder.UseSqlServer(connectionString, options =>
            {
                // Abilito il connection resiliency (Provider di SQL Server è soggetto a errori transienti)
                // Info su: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                options.EnableRetryOnFailure(3);
                options.MigrationsAssembly(typeof(TDbContext).Assembly.FullName);
            });
        });

        return services;
    }

    public static ConnectionStringsViewModel GetConnectionString(IConfigurationSection settings, ConnectionStringsInputModel inputModel)
    {
        inputModel.ConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") switch
        {
            "Development" => settings["Default"],
            _ => settings["Docker"]
                .Replace("NOME-DOCKER", settings["DockerHost"])
                .Replace("USERNAME", settings["Username"])
                .Replace("PASSWORD", settings["Password"])
        };

        return new ConnectionStringsViewModel() { ConnectionString = inputModel.ConnectionString };
    }
}