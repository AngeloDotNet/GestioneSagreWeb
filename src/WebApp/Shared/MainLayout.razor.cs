namespace GestioneSagre.Web.App.Shared;

public partial class MainLayout
{
    private MudTheme appliedTheme = new(); //https://mudblazor.com/features/colors#material-colors-list-of-material-colors
    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        appliedTheme = SetTheme();
        StateHasChanged();
    }

    private bool drawerOpen = true;
    private void DrawerToggle() => drawerOpen = !drawerOpen;

    private static MudTheme SetTheme(string theme = null) => string.IsNullOrEmpty(theme)
        ? ConfigStaticThemes.SetChosenTheme("Default")
        : ConfigStaticThemes.SetChosenTheme(theme);
}