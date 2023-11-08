using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Intestazione;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;

namespace GestioneSagre.OperazioniAvvio.BusinessLayer.Services.Intestazione;

public interface IIntestazioniService
{
    Task<Result<List<IntestazioneViewModel>>> GetListIntestazioniAsync();
    Task<Result<IntestazioneViewModel>> GetIntestazioneAsync(Guid id, Guid idFesta);
    Task<Result> CreateIntestazioneAsync(IntestazioneCreateInputModel item, CancellationToken token);
    Task<Result> EditIntestazioneAsync(IntestazioneEditInputModel item, CancellationToken token);
}