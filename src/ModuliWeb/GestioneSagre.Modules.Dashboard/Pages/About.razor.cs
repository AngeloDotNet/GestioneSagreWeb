using MudBlazor;

namespace GestioneSagre.Modules.Dashboard.Pages;

public partial class About
{
    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();

    private readonly List<BreadcrumbItem> items = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home Page", href: "/"),
        new BreadcrumbItem("About", href: null, disabled: true)
    };

    private static string YearCopyright => CalculateCurrentYear();
    private static string CalculateCurrentYear()
    {
        var startYear = 2023;
        var currentYear = DateTime.Now.Year;
        var years = startYear == currentYear ? $"{startYear}" : $"{startYear} - {currentYear}";

        return years;
    }
}