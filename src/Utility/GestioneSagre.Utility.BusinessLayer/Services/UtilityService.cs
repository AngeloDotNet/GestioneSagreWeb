namespace GestioneSagre.Utility.BusinessLayer.Services;

public class UtilityService : IUtilityService
{
    private readonly ILogger<UtilityService> logger;
    private readonly IMapper mapper;

    private readonly IUnitOfWork<ClienteTipo> uOwClienteTipo;
    private readonly IUnitOfWork<PagamentoTipo> uOwPagamentoTipo;
    private readonly IUnitOfWork<ScontrinoStato> uOwScontrinoStato;
    private readonly IUnitOfWork<ScontrinoTipo> uOwScontrinoTipo;
    private readonly IUnitOfWork<Valuta> uOwValuta;

    public UtilityService(ILogger<UtilityService> logger, IMapper mapper, IUnitOfWork<ClienteTipo> uOwClienteTipo,
        IUnitOfWork<PagamentoTipo> uOwPagamentoTipo, IUnitOfWork<ScontrinoStato> uOwScontrinoStato, IUnitOfWork<ScontrinoTipo> uOwScontrinoTipo,
        IUnitOfWork<Valuta> uOwValuta)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.uOwClienteTipo = uOwClienteTipo;
        this.uOwPagamentoTipo = uOwPagamentoTipo;
        this.uOwScontrinoStato = uOwScontrinoStato;
        this.uOwScontrinoTipo = uOwScontrinoTipo;
        this.uOwValuta = uOwValuta;
    }

    public async Task<Result<List<ClienteTipoViewModel>>> GetListClienteTipoAsync()
    {
        logger.LogInformation("Ottenimento elenco tipologie cliente");

        var options = new ItemsOptions<ClienteTipo>
        {
            Includes = null,
            ConditionWhere = null,
            OrderBy = x => x.Id,
            OrderType = OrderType.Ascending
        };

        var items = await uOwClienteTipo.Repository.GetItemsAsync(options.Includes, options.ConditionWhere,
            options.OrderBy, options.OrderType);

        if (items == null || items.Count == 0)
        {
            logger.LogWarning("Nessun valore tipo cliente trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun valore tipo cliente trovato");
        }

        var result = mapper.Map<List<ClienteTipoViewModel>>(items);

        return result;
    }

    public async Task<Result<List<PagamentoTipoViewModel>>> GetListPagamentoTipoAsync()
    {
        logger.LogInformation("Ottenimento elenco tipo pagamento");

        var options = new ItemsOptions<PagamentoTipo>
        {
            Includes = null,
            ConditionWhere = null,
            OrderBy = x => x.Id,
            OrderType = OrderType.Ascending
        };

        var items = await uOwPagamentoTipo.Repository.GetItemsAsync(options.Includes, options.ConditionWhere,
            options.OrderBy, options.OrderType);

        if (items == null || items.Count == 0)
        {
            logger.LogWarning("Nessun valore tipo pagamento trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun valore tipo pagamento trovato");
        }

        var result = mapper.Map<List<PagamentoTipoViewModel>>(items);

        return result;
    }

    public async Task<Result<List<ScontrinoStatoViewModel>>> GetListScontrinoStatoAsync()
    {
        logger.LogInformation("Ottenimento elenco stati scontrini");

        var options = new ItemsOptions<ScontrinoStato>
        {
            Includes = null,
            ConditionWhere = null,
            OrderBy = x => x.Id,
            OrderType = OrderType.Ascending
        };

        var items = await uOwScontrinoStato.Repository.GetItemsAsync(options.Includes, options.ConditionWhere,
            options.OrderBy, options.OrderType);

        if (items == null || items.Count == 0)
        {
            logger.LogWarning("Nessun valore stato scontrino trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun valore stato scontrino trovato");
        }

        var result = mapper.Map<List<ScontrinoStatoViewModel>>(items);

        return result;
    }

    public async Task<Result<List<ScontrinoTipoViewModel>>> GetListScontrinoTipoAsync()
    {
        logger.LogInformation("Ottenimento elenco tipologie scontrini");

        var options = new ItemsOptions<ScontrinoTipo>
        {
            Includes = null,
            ConditionWhere = null,
            OrderBy = x => x.Id,
            OrderType = OrderType.Ascending
        };

        var items = await uOwScontrinoTipo.Repository.GetItemsAsync(options.Includes, options.ConditionWhere,
            options.OrderBy, options.OrderType);

        if (items == null || items.Count == 0)
        {
            logger.LogWarning("Nessun valore tipo scontrino trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun valore tipo scontrino trovato");
        }

        var result = mapper.Map<List<ScontrinoTipoViewModel>>(items);

        return result;
    }

    public async Task<Result<List<ValutaViewModel>>> GetListValuteAsync()
    {
        logger.LogInformation("Ottenimento elenco Valute monetarie");

        var options = new ItemsOptions<Valuta>
        {
            Includes = null,
            ConditionWhere = null,
            OrderBy = x => x.Id,
            OrderType = OrderType.Ascending
        };

        var items = await uOwValuta.Repository.GetItemsAsync(options.Includes, options.ConditionWhere,
            options.OrderBy, options.OrderType);

        if (items == null || items.Count == 0)
        {
            logger.LogWarning("Nessun valore di valuta monetaria trovato");
            return Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun valore di valuta monetaria trovato");
        }

        var result = mapper.Map<List<ValutaViewModel>>(items);

        return result;
    }
}