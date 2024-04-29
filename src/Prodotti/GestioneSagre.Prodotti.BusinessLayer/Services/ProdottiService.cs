namespace GestioneSagre.Prodotti.BusinessLayer.Services;

public class ProdottiService : IProdottiService
{
    private readonly ILogger<ProdottiService> logger;
    private readonly IMapper mapper;

    private readonly IUnitOfWork<Prodotto> uOwProdotto;

    public ProdottiService(ILogger<ProdottiService> logger, IMapper mapper, IUnitOfWork<Prodotto> uOwProdotto)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.uOwProdotto = uOwProdotto;
    }

    public async Task<Result<List<ProdottoViewModel>>> GetListProdottiAsync()
    {
        logger.LogInformation("Ottenimento elenco Prodotti");

        var options = new ItemsOptions<Prodotto>
        {
            Includes = null,
            ConditionWhere = null,
            OrderBy = x => x.Id,
            OrderType = OrderType.Ascending
        };

        var items = await uOwProdotto.Repository.GetItemsAsync(options.Includes,
            options.ConditionWhere,
            options.OrderBy,
            options.OrderType);

        if (items == null || items.Count == 0)
        {
            logger.LogWarning("Nessun prodotto trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun prodotto trovato");
        }

        var result = mapper.Map<List<ProdottoViewModel>>(items);

        return result;
    }

    public async Task<Result<ProdottoViewModel>> GetProdottoAsync(Guid id, Guid idFesta)
    {
        logger.LogInformation("Ottenimento dettaglio prodotto");

        var options = new ItemOptions<Prodotto>
        {
            Includes = null,
            ConditionWhere = x => x.Id == id && x.IdFesta == idFesta
        };

        var item = await uOwProdotto.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessun dettaglio prodotto trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun dettaglio prodotto trovato");
        }

        var result = mapper.Map<ProdottoViewModel>(item);

        return result;
    }

    public async Task<Result> CreateProdottoAsync(ProdottoCreateInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Creazione del prodotto: " + item.Descrizione + "");

        try
        {
            var inputModel = mapper.Map<Prodotto>(item);
            await uOwProdotto.Repository.CreateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    public async Task<Result> EditProdottoAsync(ProdottoEditInputModel item, CancellationToken token)
    {
        logger.LogInformation($"Aggiornamento del prodotto: " + item.Descrizione + "");

        try
        {
            var checkProdotto = VerificaProdottoAsync(item.Id, item.IdFesta);

            if (!checkProdotto.Result)
            {
                logger.LogWarning("Nessun prodotto trovato");
                return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun prodotto trovato");
            }

            var inputModel = mapper.Map<Prodotto>(item);
            await uOwProdotto.Repository.UpdateAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    public async Task<Result> DeleteProdottoAsync(Guid id, Guid idFesta, CancellationToken token)
    {
        logger.LogInformation($"Cancellazione del prodotto: " + id + " della festa: " + idFesta + "");

        try
        {
            var checkProdotto = VerificaProdottoAsync(id, idFesta);

            if (!checkProdotto.Result)
            {
                logger.LogWarning("Nessun prodotto trovato");
                return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun prodotto trovato");
            }

            var options = new ItemOptions<Prodotto>
            {
                Includes = null,
                ConditionWhere = x => x.Id == id && x.IdFesta == idFesta
            };

            var item = await uOwProdotto.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

            var inputModel = mapper.Map<Prodotto>(item);
            await uOwProdotto.Repository.DeleteAsync(inputModel, token);

            return Result.Ok();
        }
        catch (Exception exc)
        {
            logger.LogWarning(exc.Message);
            return Result.Fail(FailureReasons.BadRequest, "Exception", detail: exc.Message);
        }
    }

    private async Task<bool> VerificaProdottoAsync(Guid id, Guid idFesta)
    {
        logger.LogInformation($"Verifica prodotto con id {id} ed idFesta {idFesta}");

        var options = new ItemOptions<Prodotto>
        {
            Includes = null,
            ConditionWhere = x => x.Id == id && x.IdFesta == idFesta
        };

        var item = await uOwProdotto.Repository.GetItemByIdAsync(options.Includes, options.ConditionWhere);

        if (item == null)
        {
            logger.LogWarning("Nessun prodotto trovato");
            return false;
        }

        logger.LogInformation("Prodotto trovato");
        return true;
    }
}