using GestioneSagre.Categorie.Shared.Models.InputModels;
using GestioneSagre.Categorie.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;

namespace GestioneSagre.Categorie.BusinessLayer.Services;

public interface ICategorieService
{
    Task<Result<List<CategoriaViewModel>>> GetListCategorieAsync();
    Task<Result<CategoriaViewModel>> GetCategoriaAsync(Guid id, Guid idFesta);
    Task<Result> CreateCategoriaAsync(CategoriaCreateInputModel item, CancellationToken token);
    Task<Result> EditCategoriaAsync(CategoriaEditInputModel item, CancellationToken token);
    Task<Result> DeleteCategoriaAsync(Guid id, Guid idFesta, CancellationToken token);
}