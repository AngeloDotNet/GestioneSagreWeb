namespace GestioneSagre.Web.App.Shared;

public partial class NavMenu
{
    [Inject] public IConfigurazioneInizialeService Service { get; set; }

    private bool isReceiptsExpanded = false;
    private bool isWarehouseExpanded = false;
    private bool isStoreExpanded = false;
    private bool isPrintsExpanded = false;
    private bool isStatsExpanded = false;

    private bool isScontrinoDisabled = true;
    private bool isReceiptsDisabled = true;
    private bool isWarehouseDisabled = true;
    private bool isStoreDisabled = true;
    private bool isPrintsDisabled = true;
    private bool isStatsDisabled = true;

    private bool isLoginDisabled = true;
    private bool isLogoutDisabled = true;
    private bool isAdminDisabled = true;

    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        var checkFeste = await Service.VerifyStatusMenu();
        var statusMenu = await Service.ManagementNavMenuAsync(checkFeste);

        isScontrinoDisabled = statusMenu.IsScontrinoDisabled;
        isReceiptsDisabled = statusMenu.IsReceiptsDisabled;
        isWarehouseDisabled = statusMenu.IsWarehouseDisabled;
        isStoreDisabled = statusMenu.IsStoreDisabled;
        isPrintsDisabled = statusMenu.IsPrintsDisabled;
        isStatsDisabled = statusMenu.IsStatsDisabled;

        isLoginDisabled = statusMenu.IsLoginDisabled;
        isLogoutDisabled = statusMenu.IsLogoutDisabled;
        isAdminDisabled = statusMenu.IsAdminDisabled;

        StateHasChanged();
    }
}