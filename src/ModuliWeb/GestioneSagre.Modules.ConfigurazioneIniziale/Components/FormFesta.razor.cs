namespace GestioneSagre.Modules.ConfigurazioneIniziale.Components;

public partial class FormFesta
{
    [Inject] public NavigationManager Navigation { get; set; }
    [Parameter] public ConfigInizialeViewModel Model { get; set; } = new();
    [Parameter] public EventCallback<ConfigInizialeViewModel> OnSave { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    private FestaStatus EnumValue { get; set; } = FestaStatus.Inactive;
    private readonly string requiredField = "Campo obbligatorio";

    public async Task SubmitAsync() => await OnSave.InvokeAsync(Model);
    public async Task CancelAsync()
    {
        Model = new();
        await OnCancel.InvokeAsync();

        Navigation.NavigateTo("/ConfigurazioneIniziale");
    }

    private void ChangeStatusGestioneMenu(bool isChecked) => Model.GestioneMenu = isChecked;
    private void ChangeStatusGestioneCategorie(bool isChecked) => Model.GestioneCategorie = isChecked;
    private void ChangeStatusStampaCarta(bool isChecked) => Model.StampaCarta = isChecked;
    private void ChangeStatusStampaRicevuta(bool isChecked) => Model.StampaRicevuta = isChecked;
}