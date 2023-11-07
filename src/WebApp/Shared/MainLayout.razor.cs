using GestioneSagre.Web.Shared.Configuration;
using MudBlazor;

namespace GestioneSagre.Web.App.Shared;

public partial class MainLayout
{
    //https://mudblazor.com/features/colors#material-colors-list-of-material-colors
    public MudTheme appliedTheme = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        appliedTheme = SetTheme();
        StateHasChanged();
    }

    bool drawerOpen = true;
    void DrawerToggle() => drawerOpen = !drawerOpen;

    private MudTheme SetTheme(string theme = null)
    {
        if (string.IsNullOrWhiteSpace(theme))
        {
            appliedTheme = ConfigStaticThemes.SetChosenTheme("Default");
        }
        else
        {
            appliedTheme = ConfigStaticThemes.SetChosenTheme(theme);
        }

        return appliedTheme;
    }
}