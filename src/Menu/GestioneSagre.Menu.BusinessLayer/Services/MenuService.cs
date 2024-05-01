using AutoMapper;
using GestioneSagre.EFCore.Models.Enums;
using GestioneSagre.EFCore.Models.Options;
using GestioneSagre.EFCore.UnitOfWork.Interfaces;
using GestioneSagre.Menu.DataAccessLayer.Entities;
using GestioneSagre.Menu.Shared.Models.InputModels;
using GestioneSagre.Menu.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;
using Microsoft.Extensions.Logging;

namespace GestioneSagre.Menu.BusinessLayer.Services;

public class MenuService : IMenuService
{
    private readonly ILogger<MenuService> logger;
    private readonly IMapper mapper;

    private readonly IUnitOfWork<Portata> uOwMenu;

    public MenuService(ILogger<MenuService> logger, IMapper mapper, IUnitOfWork<Portata> uOwMenu)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.uOwMenu = uOwMenu;
    }

    public async Task<Result<List<MenuViewModel>>> GetListMenuAsync()
    {
        logger.LogInformation("Ottenimento elenco menu");

        var options = new ItemsOptions<Portata>
        {
            Includes = null,
            ConditionWhere = null,
            OrderBy = x => x.Id,
            OrderType = OrderType.Ascending
        };

        var items = await uOwMenu.Repository.GetItemsAsync(options.Includes,
            options.ConditionWhere,
            options.OrderBy,
            options.OrderType);

        if (items == null || items.Count == 0)
        {
            logger.LogWarning("Nessun menu trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun menu trovato");
        }

        var result = mapper.Map<List<MenuViewModel>>(items);

        return result;
    }

    public async Task<Result<MenuViewModel>> GetMenuAsync(Guid id, Guid idFesta)
    {
        logger.LogInformation("Ottenimento dettaglio menu");

        var options = new ItemOptions<Portata>
        {
            Includes = null,
            ConditionWhere = x => x.Id == id && x.IdFesta == idFesta
        };

        var item = await uOwMenu.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessun menu trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun menu trovato");
        }

        var result = mapper.Map<MenuViewModel>(item);

        return result;
    }

    public async Task<Result> CreateMenuAsync(MenuCreateInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Creazione del menu del giorno: " + item.DataMenu + " della festa: " + item.IdFesta + "");

        try
        {
            var inputModel = mapper.Map<Portata>(item);
            await uOwMenu.Repository.CreateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    public async Task<Result> EditMenuAsync(MenuEditInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Aggiornamento del menu del giorno: " + item.DataMenu + " della festa: " + item.IdFesta + "");

        try
        {
            var checkCategoria = VerificaMenuAsync(item.Id, item.IdFesta);

            if (!checkCategoria.Result)
            {
                logger.LogWarning("Nessun menu trovato");
                return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun menu trovato");
            }

            var inputModel = mapper.Map<Portata>(item);
            await uOwMenu.Repository.UpdateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    public async Task<Result> DeleteMenuAsync(Guid id, Guid idFesta, CancellationToken token)
    {
        logger.LogInformation($"Cancellazione del menu: " + id + " della festa: " + idFesta + "");

        try
        {
            var checkCategoria = VerificaMenuAsync(id, idFesta);

            if (!checkCategoria.Result)
            {
                logger.LogWarning("Nessun menu trovato");
                return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun menu trovato");
            }

            var options = new ItemOptions<Portata>
            {
                Includes = null,
                ConditionWhere = x => x.Id == id && x.IdFesta == idFesta
            };

            var item = await uOwMenu.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

            var inputModel = mapper.Map<Portata>(item);
            await uOwMenu.Repository.DeleteAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    private async Task<bool> VerificaMenuAsync(Guid id, Guid idFesta)
    {
        logger.LogInformation($"Verifica menu con id {id} ed idFesta {idFesta}");

        var options = new ItemOptions<Portata>
        {
            Includes = null,
            ConditionWhere = x => x.Id == id && x.IdFesta == idFesta
        };

        var item = await uOwMenu.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessun menu trovato");
            return false;
        }

        logger.LogInformation("Menu trovato");
        return true;
    }
}