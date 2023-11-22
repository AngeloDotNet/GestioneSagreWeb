namespace GestioneSagre.Web.Shared.Components;

public partial class NavButton
{
    [Parameter, EditorRequired] public string IconButton { get; set; }
    [Parameter, EditorRequired] public string Page { get; set; }
    [Parameter, EditorRequired] public string Label { get; set; }
    [Parameter, EditorRequired] public bool Disabled { get; set; }

    public void OnButtonClicked() => navigation.NavigateTo(Page);
}