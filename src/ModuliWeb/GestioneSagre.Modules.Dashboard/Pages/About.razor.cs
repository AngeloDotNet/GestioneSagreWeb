using MudBlazor;

namespace GestioneSagre.Modules.Dashboard.Pages;

public partial class About
{
    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();

    private readonly List<BreadcrumbItem> items = new()
    {
        new BreadcrumbItem("Home Page", href: "/"),
        new BreadcrumbItem("About", href: null, disabled: true)
    };

    private static string years = string.Empty;
    private static string YearCopyright => CalculateCurrentYear(2023, DateTime.Now.Year);
    private static string CalculateCurrentYear(int startYear, int actualYear)
        => years = startYear == actualYear ? $"{startYear}" : $"{startYear} - {actualYear}";
}