namespace GestioneSagre.Web.App.Pages;

public partial class Index
{
    private readonly string appVersion = "1.0.0";
    private readonly string distroVersion = "Beta";

    private readonly List<BreadcrumbItem> items = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home Page", href: null, disabled: true)
    };
}
