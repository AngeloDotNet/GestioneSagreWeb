using AutoMapper;
using GestioneSagre.EFCore.Models.Enums;
using GestioneSagre.EFCore.Models.Options;
using GestioneSagre.EFCore.UnitOfWork.Interfaces;
using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Intestazione;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;
using Microsoft.Extensions.Logging;
using Entities = GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;

namespace GestioneSagre.OperazioniAvvio.BusinessLayer.Services.Intestazione;

public class IntestazioniService : IIntestazioniService
{
    private readonly ILogger<IntestazioniService> logger;
    private readonly IMapper mapper;

    private readonly IUnitOfWork<Entities.Intestazione> uOwIntestazione;

    public IntestazioniService(ILogger<IntestazioniService> logger, IMapper mapper, IUnitOfWork<Entities.Intestazione> uOwIntestazione)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.uOwIntestazione = uOwIntestazione;
    }

    public async Task<Result<List<IntestazioneViewModel>>> GetListIntestazioniAsync()
    {
        logger.LogInformation("Ottenimento elenco intestazioni");

        var options = new ItemsOptions<Entities.Intestazione>
        {
            Includes = null,
            ConditionWhere = null,
            OrderBy = x => x.Id,
            OrderType = OrderType.Ascending
        };

        var items = await uOwIntestazione.Repository.GetItemsAsync(options.Includes,
            options.ConditionWhere,
            options.OrderBy,
            options.OrderType);

        if (items == null || items.Count == 0)
        {
            logger.LogWarning("Nessun dettaglio delle intestazioni trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun dettaglio delle intestazioni trovato");
        }

        var result = mapper.Map<List<IntestazioneViewModel>>(items);

        return result;
    }

    public async Task<Result<IntestazioneViewModel>> GetIntestazioneAsync(Guid idFesta)
    {
        logger.LogInformation("Ottenimento dettaglio delle intestazioni della festa");

        var options = new ItemOptions<Entities.Intestazione>
        {
            Includes = null,
            ConditionWhere = x => x.IdFesta == idFesta
        };

        var item = await uOwIntestazione.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessun dettaglio delle intestazioni della festa trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun dettaglio delle intestazioni della festa trovato");
        }

        var result = mapper.Map<IntestazioneViewModel>(item);

        return result;
    }

    public async Task<Result> CreateIntestazioneAsync(IntestazioneCreateInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Creazione delle intestazioni per la festa: " + item.IdFesta + "");

        try
        {
            var inputModel = mapper.Map<Entities.Intestazione>(item);

            await uOwIntestazione.Repository.CreateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    public async Task<Result> EditIntestazioneAsync(IntestazioneEditInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Aggiornamento delle intestazioni della festa: " + item.IdFesta + "");

        try
        {
            var checkFesta = VerificaIntestazioneAsync(item.Id, item.IdFesta);

            if (!checkFesta.Result)
            {
                logger.LogWarning("Nessun dettaglio delle intestazioni trovato");
                return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun dettaglio delle intestazioni trovato");
            }

            var inputModel = mapper.Map<Entities.Intestazione>(item);
            await uOwIntestazione.Repository.UpdateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    private async Task<bool> VerificaIntestazioneAsync(Guid id, Guid idFesta)
    {
        logger.LogInformation($"Verifica delle intestazioni della festa con id {idFesta}");

        var options = new ItemOptions<Entities.Intestazione>
        {
            Includes = null,
            ConditionWhere = x => x.Id == id
        };

        var item = await uOwIntestazione.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessun dettaglio delle intestazioni della festa trovato");
            return false;
        }

        logger.LogInformation("Dettaglio delle intestazioni della festa trovato");
        return true;
    }
}