using GestioneSagre.Menu.Shared.Models.InputModels;
using GestioneSagre.Menu.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;

namespace GestioneSagre.Menu.BusinessLayer.Services;

public interface IMenuService
{
    Task<Result<List<MenuViewModel>>> GetListMenuAsync();
    Task<Result<MenuViewModel>> GetMenuAsync(Guid id, Guid idFesta);
    Task<Result> CreateMenuAsync(MenuCreateInputModel item, CancellationToken token);
    Task<Result> EditMenuAsync(MenuEditInputModel item, CancellationToken token);
    Task<Result> DeleteMenuAsync(Guid id, Guid idFesta, CancellationToken token);
}