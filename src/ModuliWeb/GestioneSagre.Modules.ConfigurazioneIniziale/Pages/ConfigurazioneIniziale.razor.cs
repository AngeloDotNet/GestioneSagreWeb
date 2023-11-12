using GestioneSagre.Web.Models.ConfigurazioneIniziale;
using GestioneSagre.Web.Services.ConfigurazioneIniziale;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestioneSagre.Modules.ConfigurazioneIniziale.Pages;

public partial class ConfigurazioneIniziale
{
    [Inject] public IConfigurazioneInizialeService Service { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await LoadDatiAsync();
    }

    private readonly List<BreadcrumbItem> items = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home Page", href: "/"),
        new BreadcrumbItem("Configurazione Iniziale", href: null, disabled: true)
    };

    private List<FestaViewModel> listItems = new();
    private string errorMessage;

    private bool isLoading = false;
    private bool disableBtnFesta = false;

    private async Task LoadDatiAsync()
    {
        try
        {
            isLoading = true;
            listItems = await Service.GetListaFeste();

            if (listItems.Any(x => x.StatusFesta is FestaStatus.Active or FestaStatus.Inactive))
            {
                disableBtnFesta = true;
                StateHasChanged();
            }
            else
            {
                disableBtnFesta = false;
                StateHasChanged();
            }
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task AttivaFestaAsync(FestaViewModel model)
    {
        try
        {
            FestaInputModel festa = new()
            {
                Id = model.Id,
                DataInizio = model.DataInizio.Value,
                DataFine = model.DataFine.Value,
                StatusFesta = FestaStatus.Active
            };

            isLoading = true;
            if (!await Service.UpdateFestaAsync(festa))
            {
                Snackbar.Add("Errore durante l'attivazione della festa !", Severity.Error);
            }

            Snackbar.Add("Festa attivata con successo !", Severity.Success);

            await LoadDatiAsync();
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task DisattivaFestaAsync(FestaViewModel model)
    {
        try
        {
            FestaInputModel festa = new()
            {
                Id = model.Id,
                DataInizio = model.DataInizio.Value,
                DataFine = model.DataFine.Value,
                StatusFesta = FestaStatus.Inactive
            };

            isLoading = true;
            if (!await Service.UpdateFestaAsync(festa))
            {
                Snackbar.Add("Errore durante la disattivazione della festa !", Severity.Error);
            }

            Snackbar.Add("Festa disattivata con successo !", Severity.Success);

            await LoadDatiAsync();
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
        finally
        {
            isLoading = false;
        }
    }

    private void ModificaFestaAsync(FestaViewModel model)
        => Navigation.NavigateTo(errorMessage is null ? $"/festa/{model.Id}" : "/configurazione-iniziale");

    private async Task TerminaFestaAsync(FestaViewModel model)
    {
        try
        {
            FestaInputModel festa = new()
            {
                Id = model.Id,
                DataInizio = model.DataInizio.Value,
                DataFine = model.DataFine.Value,
                StatusFesta = FestaStatus.Finished
            };

            isLoading = true;
            if (!await Service.UpdateFestaAsync(festa))
            {
                Snackbar.Add("Errore durante la chiusura della festa !", Severity.Error);
            }

            Snackbar.Add("Festa terminata con successo !", Severity.Success);

            await LoadDatiAsync();
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task EliminaFestaAsync(FestaViewModel model)
    {
        try
        {
            FestaInputModel festa = new()
            {
                Id = model.Id,
                DataInizio = model.DataInizio.Value,
                DataFine = model.DataFine.Value,
                StatusFesta = FestaStatus.Deleted
            };

            isLoading = true;
            if (!await Service.UpdateFestaAsync(festa))
            {
                Snackbar.Add("Errore durante l'eliminazione della festa !", Severity.Error);
            }

            Snackbar.Add("Festa eliminata con successo !", Severity.Success);

            await LoadDatiAsync();
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
        finally
        {
            isLoading = false;
        }
    }
}