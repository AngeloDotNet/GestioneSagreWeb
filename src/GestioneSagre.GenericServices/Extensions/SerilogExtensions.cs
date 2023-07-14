namespace GestioneSagre.GenericServices.Extensions;

public static class SerilogExtensions
{
    public static WebApplicationBuilder AddSerilogOptionsBuilder(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
        });

        return builder;
    }

    public static WebApplication AddSerilogConfigureServices(this WebApplication application)
    {
        application.UseSerilogRequestLogging(options =>
        {
            options.IncludeQueryInRequestPath = true;
        });

        return application;
    }

    public static IServiceCollection AddSerilogSeqServices(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().CreateLogger();

        return services;
    }
}