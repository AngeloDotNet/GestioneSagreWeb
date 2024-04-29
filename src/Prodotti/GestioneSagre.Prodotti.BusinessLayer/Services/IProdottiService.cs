namespace GestioneSagre.Prodotti.BusinessLayer.Services;

public interface IProdottiService
{
    Task<Result<List<ProdottoViewModel>>> GetListProdottiAsync();
    Task<Result<ProdottoViewModel>> GetProdottoAsync(Guid id, Guid idFesta);
    Task<Result> CreateProdottoAsync(ProdottoCreateInputModel item, CancellationToken token);
    Task<Result> EditProdottoAsync(ProdottoEditInputModel item, CancellationToken token);
    Task<Result> DeleteProdottoAsync(Guid id, Guid idFesta, CancellationToken token);
}