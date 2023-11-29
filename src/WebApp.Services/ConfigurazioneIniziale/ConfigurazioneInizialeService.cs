namespace GestioneSagre.Web.Services.ConfigurazioneIniziale;

public class ConfigurazioneInizialeService : IConfigurazioneInizialeService
{
    public HttpClient HttpClient { get; set; }

    public ConfigurazioneInizialeService(HttpClient httpClient)
    {
        this.HttpClient = httpClient;
    }

    private readonly string endpointGetAllFeste = FrontendParameters.ENDPOINT_GET_FESTE;
    private readonly string endpointGetFestaByIdFesta = FrontendParameters.ENDPOINT_GET_FESTA_ID;
    private readonly string endpointPostFesta = FrontendParameters.ENDPOINT_POST_FESTA;
    private readonly string endpointPutFesta = FrontendParameters.ENDPOINT_PUT_FESTA;

    private readonly string endpointGetIntestazioneByIdFesta = FrontendParameters.ENDPOINT_GET_INTESTAZIONE_IDFESTA;
    private readonly string endpointPostIntestazione = FrontendParameters.ENDPOINT_POST_INTESTAZIONE;
    private readonly string endpointPutIntestazione = FrontendParameters.ENDPOINT_PUT_INTESTAZIONE;

    private readonly string endpointGetImpostazioneByIdFesta = FrontendParameters.ENDPOINT_GET_IMPOSTAZIONE_IDFESTA;
    private readonly string endpointPostImpostazione = FrontendParameters.ENDPOINT_POST_IMPOSTAZIONE;
    private readonly string endpointPutImpostazione = FrontendParameters.ENDPOINT_PUT_IMPOSTAZIONE;

    #region "FESTA"
    public async Task<List<ConfigInizialeViewModel>> GetListaFeste()
    {
        try
        {
            var response = await HttpClient.GetFromJsonAsync<List<ConfigInizialeViewModel>>($"{endpointGetAllFeste}") ?? new();

            return response;
        }
        catch (HttpRequestException)
        {
            return new List<ConfigInizialeViewModel>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public async Task<ConfigInizialeViewModel> GetFestaByID(Guid id)
    {
        try
        {
            var resultFesta = await HttpClient.GetFromJsonAsync<FestaViewModel>($"{endpointGetFestaByIdFesta}/{id}") ?? new();

            var idFesta = resultFesta.Id;
            var resultIntestazione = await HttpClient.GetFromJsonAsync<IntestazioneViewModel>($"{endpointGetIntestazioneByIdFesta}/{idFesta}") ?? new();
            var resultImpostazione = await HttpClient.GetFromJsonAsync<ImpostazioneViewModel>($"{endpointGetImpostazioneByIdFesta}/{idFesta}") ?? new();

            var result = new ConfigInizialeViewModel()
            {
                Id = resultFesta.Id,
                DataInizio = resultFesta.DataInizio,
                DataFine = resultFesta.DataFine,
                StatusFesta = resultFesta.StatusFesta,

                IdIntestazione = resultIntestazione.Id,
                IdFesta = resultIntestazione.IdFesta,
                Titolo = resultIntestazione.Titolo,
                Edizione = resultIntestazione.Edizione,
                Luogo = resultIntestazione.Luogo,

                IdImpostazione = resultImpostazione.Id,
                GestioneMenu = resultImpostazione.GestioneMenu,
                GestioneCategorie = resultImpostazione.GestioneCategorie,
                StampaCarta = resultImpostazione.StampaCarta,
                StampaRicevuta = resultImpostazione.StampaRicevuta
            };

            return result;
        }
        catch (HttpRequestException)
        {
            return new ConfigInizialeViewModel();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public async Task<bool> CreateNuovaFestaAsync(FestaInputModel model)
    {
        var response = await HttpClient.PostAsJsonAsync($"{endpointPostFesta}", model);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateFestaAsync(FestaInputModel model)
    {
        var response = await HttpClient.PutAsJsonAsync($"{endpointPutFesta}", model);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }
    #endregion

    #region "INTESTAZIONE"
    public async Task<bool> CreateNuovaIntestazioneAsync(IntestazioneInputModel model)
    {
        var response = await HttpClient.PostAsJsonAsync($"{endpointPostIntestazione}", model);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateIntestazioneAsync(IntestazioneInputModel model)
    {
        var response = await HttpClient.PutAsJsonAsync($"{endpointPutIntestazione}", model);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }
    #endregion

    #region "IMPOSTAZIONE"
    public async Task<bool> CreateNuovaImpostazioneAsync(ImpostazioneInputModel model)
    {
        var response = await HttpClient.PostAsJsonAsync($"{endpointPostImpostazione}", model);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateImpostazioneAsync(ImpostazioneInputModel model)
    {
        var response = await HttpClient.PutAsJsonAsync($"{endpointPutImpostazione}", model);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }
    #endregion

    public async Task<ManageNavMenu> ManagementNavMenuAsync(bool enabledMenu)
    {
        await Task.Delay(500);

        if (enabledMenu == true)
        {
            return new ManageNavMenu()
            {
                IsScontrinoDisabled = false,
                IsReceiptsDisabled = false,
                IsWarehouseDisabled = false,
                IsStoreDisabled = false,
                IsPrintsDisabled = false,
                IsStatsDisabled = false,

                IsLoginDisabled = true,
                IsLogoutDisabled = true,
                IsAdminDisabled = true
            };
        }
        else
        {
            return new ManageNavMenu()
            {
                IsScontrinoDisabled = true,
                IsReceiptsDisabled = true,
                IsWarehouseDisabled = true,
                IsStoreDisabled = true,
                IsPrintsDisabled = true,
                IsStatsDisabled = true,

                IsLoginDisabled = true,
                IsLogoutDisabled = true,
                IsAdminDisabled = true
            };
        }
    }

    public async Task<bool> VerifyStatusMenu()
    {
        var LoadingFesteList = await GetListaFeste() ?? new();

        if (LoadingFesteList.Count == 0 || (LoadingFesteList.Any(y => !CheckStatusFesta(y.StatusFesta))))
        {
            return false;
        }

        if (LoadingFesteList.Any(y => CheckStatusFesta(y.StatusFesta)))
        {
            return true;
        }

        return false;
    }

    private static bool CheckStatusFesta(FestaStatus status)
    {
        if (status == FestaStatus.Active)
        {
            return true;
        }

        return false;
    }
}