using GestioneSagre.Web.Models.ConfigurazioneIniziale;
using Microsoft.AspNetCore.Components;

namespace GestioneSagre.Modules.ConfigurazioneIniziale.Components;

public partial class FormFesta
{
    [Inject] public NavigationManager Navigation { get; set; }
    [Parameter] public FestaViewModel Model { get; set; } = new();
    [Parameter] public EventCallback<FestaViewModel> OnSave { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    private FestaStatus EnumValue { get; set; } = FestaStatus.Inactive;

    public async Task SubmitAsync() => await OnSave.InvokeAsync(Model);
    public async Task CancelAsync()
    {
        Model = new();
        await OnCancel.InvokeAsync();

        Navigation.NavigateTo("/ConfigurazioneIniziale");
    }
}