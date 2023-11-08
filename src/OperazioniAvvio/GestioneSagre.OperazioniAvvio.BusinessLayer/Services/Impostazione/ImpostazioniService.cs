using AutoMapper;
using GestioneSagre.EFCore.Models.Enums;
using GestioneSagre.EFCore.Models.Options;
using GestioneSagre.EFCore.UnitOfWork.Interfaces;
using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Impostazione;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;
using Microsoft.Extensions.Logging;
using Entities = GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;

namespace GestioneSagre.OperazioniAvvio.BusinessLayer.Services.Impostazione;

public class ImpostazioniService : IImpostazioniService
{
    private readonly ILogger<ImpostazioniService> logger;
    private readonly IMapper mapper;

    private readonly IUnitOfWork<Entities.Impostazione> uOwImpostazione;

    public ImpostazioniService(ILogger<ImpostazioniService> logger, IMapper mapper, IUnitOfWork<Entities.Impostazione> uOwImpostazione)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.uOwImpostazione = uOwImpostazione;
    }

    public async Task<Result<List<ImpostazioneViewModel>>> GetListImpostazioniAsync()
    {
        logger.LogInformation("Ottenimento elenco impostazioni");

        var options = new ItemsOptions<Entities.Impostazione>
        {
            Includes = null,
            ConditionWhere = null,
            OrderBy = x => x.Id,
            OrderType = OrderType.Ascending
        };

        var items = await uOwImpostazione.Repository.GetItemsAsync(options.Includes,
            options.ConditionWhere,
            options.OrderBy,
            options.OrderType);

        if (items == null || items.Count == 0)
        {
            logger.LogWarning("Nessun dettaglio delle impostazioni trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun dettaglio delle impostazioni trovato");
        }

        var result = mapper.Map<List<ImpostazioneViewModel>>(items);

        return result;
    }

    public async Task<Result<ImpostazioneViewModel>> GetImpostazioneAsync(Guid id, Guid idFesta)
    {
        logger.LogInformation("Ottenimento dettaglio delle impostazioni della festa");

        var options = new ItemOptions<Entities.Impostazione>
        {
            Includes = null,
            ConditionWhere = x => x.Id == id
        };

        var item = await uOwImpostazione.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessun dettaglio delle impostazioni della festa trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun dettaglio delle impostazioni della festa trovato");
        }

        var result = mapper.Map<ImpostazioneViewModel>(item);

        return result;
    }

    public async Task<Result> CreateImpostazioneAsync(ImpostazioneCreateInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Creazione della festa: " + item.IdFesta + "");

        try
        {
            var inputModel = mapper.Map<Entities.Impostazione>(item);

            await uOwImpostazione.Repository.CreateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    public async Task<Result> EditImpostazioneAsync(ImpostazioneEditInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Aggiornamento delle impostazioni della festa: " + item.IdFesta + "");

        try
        {
            var checkFesta = VerificaImpostazioneAsync(item.Id, item.IdFesta);

            if (!checkFesta.Result)
            {
                logger.LogWarning("Nessun dettaglio delle impostazioni trovato");
                return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun dettaglio delle impostazioni trovato");
            }

            var inputModel = mapper.Map<Entities.Impostazione>(item);
            await uOwImpostazione.Repository.UpdateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    private async Task<bool> VerificaImpostazioneAsync(Guid id, Guid idFesta)
    {
        logger.LogInformation($"Verifica delle impostazioni della festa con id {idFesta}");

        var options = new ItemOptions<Entities.Impostazione>
        {
            Includes = null,
            ConditionWhere = x => x.Id == id
        };

        var item = await uOwImpostazione.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessun dettaglio delle impostazioni della festa trovato");
            return false;
        }

        logger.LogInformation("Dettaglio delle impostazioni della festa trovato");
        return true;
    }
}