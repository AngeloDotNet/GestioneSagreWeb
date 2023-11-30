using AutoMapper;
using GestioneSagre.Categorie.DataAccessLayer.Entities;
using GestioneSagre.Categorie.Shared.Models.InputModels;
using GestioneSagre.Categorie.Shared.Models.ViewModels;
using GestioneSagre.EFCore.Models.Enums;
using GestioneSagre.EFCore.Models.Options;
using GestioneSagre.EFCore.UnitOfWork.Interfaces;
using GestioneSagre.Shared.OperationResults;
using Microsoft.Extensions.Logging;

namespace GestioneSagre.Categorie.BusinessLayer.Services;

public class CategorieService : ICategorieService
{
    private readonly ILogger<CategorieService> logger;
    private readonly IMapper mapper;

    private readonly IUnitOfWork<Categoria> uOwCategoria;

    public CategorieService(ILogger<CategorieService> logger, IMapper mapper, IUnitOfWork<Categoria> uOwCategoria)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.uOwCategoria = uOwCategoria;
    }

    public async Task<Result<List<CategoriaViewModel>>> GetListCategorieAsync()
    {
        logger.LogInformation("Ottenimento elenco categorie");

        var options = new ItemsOptions<Categoria>
        {
            Includes = null,
            ConditionWhere = null,
            OrderBy = x => x.Id,
            OrderType = OrderType.Ascending
        };

        var items = await uOwCategoria.Repository.GetItemsAsync(options.Includes,
            options.ConditionWhere,
            options.OrderBy,
            options.OrderType);

        if (items == null || items.Count == 0)
        {
            logger.LogWarning("Nessuna categoria trovata");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessuna categoria trovata");
        }

        var result = mapper.Map<List<CategoriaViewModel>>(items);

        return result;
    }

    public async Task<Result<CategoriaViewModel>> GetCategoriaAsync(Guid id, Guid idFesta)
    {
        logger.LogInformation("Ottenimento dettaglio categoria");

        var options = new ItemOptions<Categoria>
        {
            Includes = null,
            ConditionWhere = x => x.Id == id && x.IdFesta == idFesta
        };

        var item = await uOwCategoria.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessuna categoria trovata");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessuna categoria trovata");
        }

        var result = mapper.Map<CategoriaViewModel>(item);

        return result;
    }

    public async Task<Result> CreateCategoriaAsync(CategoriaCreateInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Creazione della categoria: " + item.CategoriaVideo + " della festa: " + item.IdFesta + "");

        try
        {
            var inputModel = mapper.Map<Categoria>(item);
            await uOwCategoria.Repository.CreateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    public async Task<Result> EditCategoriaAsync(CategoriaEditInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Aggiornamento della categoria: " + item.CategoriaVideo + " della festa: " + item.IdFesta + "");

        try
        {
            var checkCategoria = VerificaCategoriaAsync(item.Id, item.IdFesta);

            if (!checkCategoria.Result)
            {
                logger.LogWarning("Nessuna categoria trovata");
                return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessuna categoria trovata");
            }

            var inputModel = mapper.Map<Categoria>(item);
            await uOwCategoria.Repository.UpdateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    public async Task<Result> DeleteCategoriaAsync(Guid id, Guid idFesta, CancellationToken token)
    {
        logger.LogInformation($"Cancellazione della categoria: " + id + " della festa: " + idFesta + "");

        try
        {
            var checkCategoria = VerificaCategoriaAsync(id, idFesta);

            if (!checkCategoria.Result)
            {
                logger.LogWarning("Nessuna categoria trovata");
                return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessuna categoria trovata");
            }

            var options = new ItemOptions<Categoria>
            {
                Includes = null,
                ConditionWhere = x => x.Id == id && x.IdFesta == idFesta
            };

            var item = await uOwCategoria.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

            var inputModel = mapper.Map<Categoria>(item);
            await uOwCategoria.Repository.DeleteAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    private async Task<bool> VerificaCategoriaAsync(Guid id, Guid idFesta)
    {
        logger.LogInformation($"Verifica categoria con id {id} ed idFesta {idFesta}");

        var options = new ItemOptions<Categoria>
        {
            Includes = null,
            ConditionWhere = x => x.Id == id && x.IdFesta == idFesta
        };

        var item = await uOwCategoria.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessuna categoria trovata");
            return false;
        }

        logger.LogInformation("Categoria trovata");
        return true;
    }
}