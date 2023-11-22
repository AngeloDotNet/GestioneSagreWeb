namespace GestioneSagre.Web.App.Components;

public partial class Welcome
{
    [Parameter] public string Version { get; set; } = string.Empty;
    [Parameter] public string Channel { get; set; } = string.Empty;
}