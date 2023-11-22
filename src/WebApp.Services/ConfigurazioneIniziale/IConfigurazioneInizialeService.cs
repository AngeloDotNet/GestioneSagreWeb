namespace GestioneSagre.Web.Services.ConfigurazioneIniziale;

public interface IConfigurazioneInizialeService
{
    Task<List<ConfigInizialeViewModel>> GetListaFeste();
    Task<ConfigInizialeViewModel> GetFestaByID(Guid id);
    Task<bool> CreateNuovaFestaAsync(FestaInputModel model);
    Task<bool> UpdateFestaAsync(FestaInputModel model);
    Task<bool> CreateNuovaIntestazioneAsync(IntestazioneInputModel model);
    Task<bool> UpdateIntestazioneAsync(IntestazioneInputModel model);
    Task<bool> CreateNuovaImpostazioneAsync(ImpostazioneInputModel model);
    Task<bool> UpdateImpostazioneAsync(ImpostazioneInputModel model);
    Task<ManageNavMenu> ManagementNavMenuAsync(bool enabledMenu);
    Task<bool> VerifyStatusMenu();
}