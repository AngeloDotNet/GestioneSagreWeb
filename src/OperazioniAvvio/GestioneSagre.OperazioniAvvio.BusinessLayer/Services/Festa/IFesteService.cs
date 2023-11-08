using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Festa;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;

namespace GestioneSagre.OperazioniAvvio.BusinessLayer.Services.Festa;

public interface IFesteService
{
    Task<Result<List<FestaViewModel>>> GetListFesteAsync();
    Task<Result<FestaViewModel>> GetFestaAsync(Guid id);
    Task<Result> CreateFestaAsync(FestaCreateInputModel item, CancellationToken token);
    Task<Result> EditFestaAsync(FestaEditInputModel item, CancellationToken token);
    Task<Result> DeleteFestaAsync(Guid id, CancellationToken token);
}