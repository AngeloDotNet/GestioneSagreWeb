using GestioneSagre.GenericServices.Extensions;

namespace GestioneSagre.Aggregator;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddSerilogOptionsBuilder();

        Startup startup = new(builder.Configuration);

        startup.ConfigureServices(builder.Services);

        var app = builder.Build();

        startup.Configure(app);

        app.Run();
    }
}

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        CreateHostBuilder(args).Build().Run();
//    }

//    public static IHostBuilder CreateHostBuilder(string[] args) =>
//        Host.CreateDefaultBuilder(args)
//            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
//}