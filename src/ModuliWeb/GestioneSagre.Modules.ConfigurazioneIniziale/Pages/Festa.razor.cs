using GestioneSagre.Web.Models.ConfigurazioneIniziale;
using GestioneSagre.Web.Services.ConfigurazioneIniziale;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestioneSagre.Modules.ConfigurazioneIniziale.Pages;

public partial class Festa
{
    [Inject] public IConfigurazioneInizialeService Service { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }
    [Parameter] public Guid Id { get; set; }

    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();

    private readonly List<BreadcrumbItem> items = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home Page", href: "/"),
        new BreadcrumbItem("Configurazione Iniziale", href: "/ConfigurazioneIniziale"),
        new BreadcrumbItem("Gestione Festa", href: null, disabled: true)
    };

    private readonly FestaViewModel model = new();
    private string errorMessage;

    private async Task SaveFestaAsync(FestaViewModel model)
    {
        try
        {
            model.Id = Guid.NewGuid();
            var inputModel = CheckAndConvertModelForApi(model);

            if (!await Service.CreateNuovaFestaAsync(inputModel))
            {
                Snackbar.Add("Errore durante la creazione della festa !", Severity.Error);
            }

            Snackbar.Add("Festa creata con successo !", Severity.Success);

            Navigation.NavigateTo("/ConfigurazioneIniziale");
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
        catch (ArgumentNullException ex)
        {
            errorMessage = ex.Message;
        }
    }

    private async Task UpdateFestaAsync(FestaViewModel model)
    {
        try
        {
            var inputModel = CheckAndConvertModelForApi(model);

            if (!await Service.UpdateFestaAsync(inputModel))
            {
                Snackbar.Add("Errore durante l'aggiornamento della festa !", Severity.Error);
            }

            Snackbar.Add("Festa aggiornata con successo !", Severity.Success);

            Navigation.NavigateTo("/ConfigurazioneIniziale");
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
        catch (ArgumentNullException ex)
        {
            errorMessage = ex.Message;
        }
    }

    private static FestaInputModel CheckAndConvertModelForApi(FestaViewModel model)
    {
        if (model.DataInizio is null)
        {
            throw new ArgumentNullException($"Il campo {nameof(model.DataInizio)} è obbligatorio");
        }

        if (model.DataFine is null)
        {
            throw new ArgumentNullException($"Il campo {nameof(model.DataFine)} è obbligatorio");
        }

        if (model.DataInizio.Value > model.DataFine.Value)
        {
            throw new ApplicationException($"Il campo {nameof(model.DataInizio)} non può essere maggiore del campo {nameof(model.DataFine)}");
        }

        if (model.DataFine.Value < model.DataInizio.Value)
        {
            throw new ApplicationException($"Il campo {nameof(model.DataFine)} non può essere minore del campo {nameof(model.DataInizio)}");
        }

        return new FestaInputModel
        {
            Id = model.Id,
            DataInizio = model.DataInizio.Value,
            DataFine = model.DataFine.Value,
            StatusFesta = model.StatusFesta
        };
    }
}