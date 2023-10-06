using GestioneSagre.GenericServices.Extensions;

namespace GestioneSagre.Gateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddSerilogOptionsBuilder();
        builder.Configuration
            .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
            .AddJsonFile("ocelot-categorie.json", optional: false, reloadOnChange: true)
            .AddJsonFile("ocelot-dataprotection.json", optional: false, reloadOnChange: true)
            .AddJsonFile("ocelot-prodotti.json", optional: false, reloadOnChange: true)
            .AddJsonFile("ocelot-utility.json", optional: false, reloadOnChange: true);

        Startup startup = new(builder.Configuration);

        startup.ConfigureServices(builder.Services);

        var app = builder.Build();

        startup.Configure(app);

        app.Run();
    }
}