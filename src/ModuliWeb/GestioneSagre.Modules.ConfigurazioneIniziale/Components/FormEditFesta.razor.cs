using GestioneSagre.Web.Models.ConfigurazioneIniziale;
using GestioneSagre.Web.Services.ConfigurazioneIniziale;
using Microsoft.AspNetCore.Components;

namespace GestioneSagre.Modules.ConfigurazioneIniziale.Components;

public partial class FormEditFesta
{
    [Inject] public IConfigurazioneInizialeService Service { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Parameter, EditorRequired] public Guid IdDetail { get; set; }
    [Parameter] public EventCallback<FestaViewModel> OnSave { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    private FestaViewModel Model { get; set; } = new();

    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Model = await Service.GetFestaByID(IdDetail);
    }

    public async Task SubmitAsync() => await OnSave.InvokeAsync(Model);
    public void CancelAsync() => Navigation.NavigateTo("/ConfigurazioneIniziale");
}