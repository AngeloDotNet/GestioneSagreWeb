namespace GestioneSagre.Web.Shared.Components;

public partial class HeaderPage
{
    [Parameter] public List<BreadcrumbItem> itemsList { get; set; } = new();
}
