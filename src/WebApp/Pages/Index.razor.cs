namespace GestioneSagre.Web.App.Pages;

public partial class Index
{
    private readonly string appVersion = "1.0.0";
    private readonly string distroVersion = "Beta"; //Alpha, Beta, RC, Final

    private readonly List<BreadcrumbItem> items = new()
    {
        new BreadcrumbItem("Home Page", href: null, disabled: true)
    };
}