namespace GestioneSagre.Modules.ConfigurazioneIniziale.Pages;

public partial class Festa
{
    [Inject] public IConfigurazioneInizialeService Service { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }
    [Parameter] public Guid Id { get; set; }

    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();

    private readonly List<BreadcrumbItem> items = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home Page", href: "/"),
        new BreadcrumbItem("Configurazione Iniziale", href: "configurazioneiniziale"),
        new BreadcrumbItem("Gestione Festa", href: null, disabled: true)
    };

    private ConfigInizialeViewModel model = new();

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        model = await Service.GetFestaByID(Id);
    }

    public string errorMessage;

    private async Task SaveFestaAsync(ConfigInizialeViewModel model)
    {
        try
        {
            model.Id = Guid.NewGuid();
            model.IdFesta = model.Id;

            var inputModelImpostazione = CheckConvertImpostazioneModelApi(model);
            var inputModelIntestazione = CheckConvertIntestazioneModelApi(model);
            var inputModelFesta = CheckConvertFestaModelApi(model);

            if (!await Service.CreateNuovaImpostazioneAsync(inputModelImpostazione))
            {
                Snackbar.Add("Errore durante la creazione dell'impostazione !", Severity.Error);
            }
            else
            {
                if (!await Service.CreateNuovaIntestazioneAsync(inputModelIntestazione))
                {
                    Snackbar.Add("Errore durante la creazione dell'intestazione !", Severity.Error);
                }
                else
                {
                    if (!await Service.CreateNuovaFestaAsync(inputModelFesta))
                    {
                        Snackbar.Add("Errore durante la creazione della festa !", Severity.Error);
                    }
                    else
                    {
                        Snackbar.Add("Festa creata con successo !", Severity.Success);

                        Navigation.NavigateTo("/ConfigurazioneIniziale");
                    }
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            errorMessage = ex.Message;
        }
        catch (ApplicationException ex)
        {
            errorMessage = ex.Message;
        }
    }

    private async Task UpdateFestaAsync(ConfigInizialeViewModel model)
    {
        var inputModelImpostazione = CheckConvertImpostazioneModelApi(model);
        var inputModelIntestazione = CheckConvertIntestazioneModelApi(model);
        var inputModelFesta = CheckConvertFestaModelApi(model);

        try
        {
            if (!await Service.UpdateImpostazioneAsync(inputModelImpostazione))
            {
                Snackbar.Add("Errore durante l'aggiornamento dell'impostazione !", Severity.Error);
            }
            else
            {
                if (!await Service.UpdateIntestazioneAsync(inputModelIntestazione))
                {
                    Snackbar.Add("Errore durante l'aggiornamento dell'intestazione !", Severity.Error);
                }
                else
                {
                    if (!await Service.UpdateFestaAsync(inputModelFesta))
                    {
                        Snackbar.Add("Errore durante l'aggiornamento della festa !", Severity.Error);
                    }
                    else
                    {
                        Snackbar.Add("Festa aggiornata con successo !", Severity.Success);
                        Navigation.NavigateTo("/ConfigurazioneIniziale");
                    }
                }
            }
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

    private static FestaInputModel CheckConvertFestaModelApi(ConfigInizialeViewModel model)
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

    private static IntestazioneInputModel CheckConvertIntestazioneModelApi(ConfigInizialeViewModel model)
    {
        if (model.Titolo is null)
        {
            throw new ArgumentNullException($"Il campo {nameof(model.Titolo)} è obbligatorio");
        }

        if (model.Edizione is null)
        {
            throw new ArgumentNullException($"Il campo {nameof(model.Edizione)} è obbligatorio");
        }

        if (model.Luogo is null)
        {
            throw new ArgumentNullException($"Il campo {nameof(model.Luogo)} è obbligatorio");
        }

        return new IntestazioneInputModel
        {
            Id = model.IdIntestazione,
            IdFesta = model.IdFesta,
            Titolo = model.Titolo,
            Edizione = model.Edizione,
            Luogo = model.Luogo
        };
    }

    public static ImpostazioneInputModel CheckConvertImpostazioneModelApi(ConfigInizialeViewModel model)
    {
        return new ImpostazioneInputModel
        {
            Id = model.IdImpostazione,
            IdFesta = model.IdFesta,
            GestioneMenu = model.GestioneMenu,
            GestioneCategorie = model.GestioneCategorie,
            StampaCarta = model.StampaCarta,
            StampaRicevuta = model.StampaRicevuta
        };
    }
}