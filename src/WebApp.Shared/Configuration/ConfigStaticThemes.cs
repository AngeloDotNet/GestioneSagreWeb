namespace GestioneSagre.Web.Shared.Configuration;

public static class ConfigStaticThemes
{
    public static MudTheme appliedTheme = new();

    public static MudTheme SetChosenTheme(string theme)
    {
        appliedTheme = theme switch
        {
            //"Default" => GenerateDefaultTheme(),
            //"Purple" => GeneratePurpleTheme(),
            _ => GenerateDefaultTheme(),
        };
        return appliedTheme;
    }

    private static MudTheme GenerateDefaultTheme() =>
        new MudTheme
        {
            Palette = new()
            {
                AppbarBackground = Colors.Grey.Default
            }
        };

    //private static MudTheme GeneratePurpleTheme() =>
    //    new MudTheme
    //    {
    //        Palette = new()
    //        {
    //            AppbarBackground = Colors.Purple.Default
    //        }
    //    };
}