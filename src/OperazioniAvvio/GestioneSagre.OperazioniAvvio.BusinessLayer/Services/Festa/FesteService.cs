using AutoMapper;
using GestioneSagre.EFCore.Models.Enums;
using GestioneSagre.EFCore.Models.Options;
using GestioneSagre.EFCore.UnitOfWork.Interfaces;
using GestioneSagre.OperazioniAvvio.Shared.Models.Enums;
using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Festa;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;
using Microsoft.Extensions.Logging;
using Entities = GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;

namespace GestioneSagre.OperazioniAvvio.BusinessLayer.Services.Festa;

public class FesteService : IFesteService
{
    private readonly ILogger<FesteService> logger;
    private readonly IMapper mapper;

    private readonly IUnitOfWork<Entities.Festa> uOwFesta;

    public FesteService(ILogger<FesteService> logger, IMapper mapper, IUnitOfWork<Entities.Festa> uOwFesta)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.uOwFesta = uOwFesta;
    }

    public async Task<Result<List<FestaViewModel>>> GetListFesteAsync()
    {
        logger.LogInformation("Ottenimento elenco delle feste");

        var options = new ItemsOptions<Entities.Festa>
        {
            Includes = null,
            ConditionWhere = null,
            OrderBy = x => x.Id,
            OrderType = OrderType.Ascending
        };

        var items = await uOwFesta.Repository.GetItemsAsync(options.Includes,
            options.ConditionWhere,
            options.OrderBy,
            options.OrderType);

        if (items == null || items.Count == 0)
        {
            logger.LogWarning("Nessuna festa configurata trovata");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessuna festa configurata trovata");
        }

        var result = mapper.Map<List<FestaViewModel>>(items);

        return result;
    }

    public async Task<Result<FestaViewModel>> GetFestaAsync(Guid id)
    {
        logger.LogInformation("Ottenimento dettaglio festa");

        var options = new ItemOptions<Entities.Festa>
        {
            Includes = null,
            ConditionWhere = x => x.Id == id
        };

        var item = await uOwFesta.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessun dettaglio festa trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun dettaglio festa trovato");
        }

        var result = mapper.Map<FestaViewModel>(item);

        return result;
    }

    public async Task<Result> CreateFestaAsync(FestaCreateInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Creazione della festa: " + item.Id + "");

        try
        {
            var inputModel = mapper.Map<Entities.Festa>(item);
            inputModel.StatusFesta = FestaStatus.Inactive;

            await uOwFesta.Repository.CreateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    public async Task<Result> EditFestaAsync(FestaEditInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Aggiornamento della festa: " + item.Id + "");

        try
        {
            var checkFesta = VerificaFestaAsync(item.Id);

            if (!checkFesta.Result)
            {
                logger.LogWarning("Nessun dettaglio della festa trovato");
                return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun dettaglio della festa trovato");
            }

            var inputModel = mapper.Map<Entities.Festa>(item);
            await uOwFesta.Repository.UpdateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    public async Task<Result> DeleteFestaAsync(Guid id, CancellationToken token)
    {
        logger.LogInformation($"Cancellazione della festa: " + id + "");

        try
        {
            var checkFesta = VerificaFestaAsync(id);

            if (!checkFesta.Result)
            {
                logger.LogWarning("Nessun dettaglio della festa trovato");
                return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun dettaglio della festa trovato");
            }

            var options = new ItemOptions<Entities.Festa>
            {
                Includes = null,
                ConditionWhere = x => x.Id == id
            };

            var item = await uOwFesta.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

            var inputModel = mapper.Map<Entities.Festa>(item);
            inputModel.StatusFesta = FestaStatus.Deleted;

            await uOwFesta.Repository.UpdateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    private async Task<bool> VerificaFestaAsync(Guid id)
    {
        logger.LogInformation($"Verifica festa con id {id}");

        var options = new ItemOptions<Entities.Festa>
        {
            Includes = null,
            ConditionWhere = x => x.Id == id
        };

        var item = await uOwFesta.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessun dettaglio festa trovato");
            return false;
        }

        logger.LogInformation("Dettaglio festa trovato");
        return true;
    }
}