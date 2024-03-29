namespace GestioneSagre.Gateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddSerilogOptionsBuilder();
        builder.Configuration.AddOcelot("ConfigOcelot", builder.Environment);

        Startup startup = new(builder.Configuration);

        startup.ConfigureServices(builder.Services);

        var app = builder.Build();

        startup.Configure(app);

        app.Run();
    }
}