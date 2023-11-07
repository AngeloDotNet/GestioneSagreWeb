using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

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
                //case "fetchdata":
                //    {
                //        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                //        {
                //            "MultiProjectsRoute.Modules.WeatherForecast.dll"
                //        });
                //        LazyLoadedAssemblies.AddRange(assemblies);
                //        break;
                //    }

                //case "counter":
                //    {
                //        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                //        {
                //            "MultiProjectsRoute.Modules.Counter.dll"
                //        });
                //        LazyLoadedAssemblies.AddRange(assemblies);
                //        break;
                //    }

                default:
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                        {
                            "GestioneSagre.Modules.Dashboard.dll"
                        });
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

    #region Dispose
    public void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~AppBase()
    {
        this.Dispose(false);
    }
    #endregion
}