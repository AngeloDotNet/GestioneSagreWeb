namespace GestioneSagre.Modules.ConfigurazioneIniziale.Components;

public partial class FormEditFesta
{
    [Inject] public IConfigurazioneInizialeService Service { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Parameter, EditorRequired] public Guid IdDetail { get; set; }
    [Parameter] public EventCallback<ConfigInizialeViewModel> OnSave { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    public ConfigInizialeViewModel Model = new();
    private readonly string requiredField = "Campo obbligatorio";

    protected override void OnInitialized() => base.OnInitialized();

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        Model = await Service.GetFestaByID(IdDetail);
    }

    public async Task SubmitAsync() => await OnSave.InvokeAsync(Model);
    public void CancelAsync() => Navigation.NavigateTo("/ConfigurazioneIniziale");

    private void ChangeStatusGestioneMenu(bool isChecked) => Model.GestioneMenu = isChecked;
    private void ChangeStatusGestioneCategorie(bool isChecked) => Model.GestioneCategorie = isChecked;
    private void ChangeStatusStampaCarta(bool isChecked) => Model.StampaCarta = isChecked;
    private void ChangeStatusStampaRicevuta(bool isChecked) => Model.StampaRicevuta = isChecked;
}