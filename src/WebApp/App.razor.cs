namespace GestioneSagre.Web.App;

public class AppBase : ComponentBase, IDisposable
{
    [Inject] private LazyAssemblyLoader AssemblyLoader { get; set; }
    [Inject] private ILogger<App> Logger { get; set; }

    protected readonly List<Assembly> LazyLoadedAssemblies = new();
    protected async Task OnNavigateAsync(NavigationContext args)
    {
        try
        {
            switch (args.Path)
            {
                case "configurazioneiniziale":
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new[] { "GestioneSagre.Modules.ConfigurazioneIniziale.dll" });
                        LazyLoadedAssemblies.AddRange(assemblies);

                        break;
                    }

                //case "categorie":
                //    {
                //        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new[] { "GestioneSagre.Modules.Categorie.dll" });
                //        LazyLoadedAssemblies.AddRange(assemblies);

                //        break;
                //    }

                default:
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new[] { "GestioneSagre.Modules.Dashboard.dll" });
                        LazyLoadedAssemblies.AddRange(assemblies);

                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Error Loading spares page: {ex}");
        }
    }

    public static void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~AppBase()
    {
        Dispose(false);
    }
}