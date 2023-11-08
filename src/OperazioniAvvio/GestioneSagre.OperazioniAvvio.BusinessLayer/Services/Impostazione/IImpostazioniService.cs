using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Impostazione;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;

namespace GestioneSagre.OperazioniAvvio.BusinessLayer.Services.Impostazione;

public interface IImpostazioniService
{
    Task<Result<List<ImpostazioneViewModel>>> GetListImpostazioniAsync();
    Task<Result<ImpostazioneViewModel>> GetImpostazioneAsync(Guid id, Guid idFesta);
    Task<Result> CreateImpostazioneAsync(ImpostazioneCreateInputModel item, CancellationToken token);
    Task<Result> EditImpostazioneAsync(ImpostazioneEditInputModel item, CancellationToken token);
}