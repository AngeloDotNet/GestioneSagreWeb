using GestioneSagre.GenericServices.Extensions;

namespace GestioneSagre.OperazioniAvvio;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args)
            .AddSerilogOptionsBuilder();

        Startup startup = new(builder.Configuration);

        startup.ConfigureServices(builder.Services);

        var app = builder.Build();

        startup.Configure(app);

        app.Run();
    }
}