namespace GestioneSagre.Web.Shared.Components;

public partial class HeaderPage
{
    [Parameter] public List<BreadcrumbItem> ItemsList { get; set; } = new();
}
