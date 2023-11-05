namespace GestioneSagre.Web.App.Shared;

public partial class NavMenu
{
    private bool isStartExpanded = false;
    private bool isWarehouseExpanded = false;
    private bool isStoreExpanded = false;
    private bool isReceiptsExpanded = false;
    private bool isPrintsExpanded = false;
    private bool isStatsExpanded = false;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        StateHasChanged();
    }
}