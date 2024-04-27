namespace GestioneSagre.Utility.Controllers;

public class UtilityController : BaseController
{
    private readonly IUtilityService utilityService;

    public UtilityController(IUtilityService utilityService)
    {
        this.utilityService = utilityService;
    }

    [HttpGet("TipoCliente")]
    [ProducesResponseType(typeof(List<ClienteTipoViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTipoClienteAsync()
    {
        try
        {
            var result = await utilityService.GetListClienteTipoAsync();
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpGet("TipoPagamento")]
    [ProducesResponseType(typeof(List<PagamentoTipoViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTipoPagamentoAsync()
    {
        try
        {
            var result = await utilityService.GetListPagamentoTipoAsync();
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpGet("StatoScontrino")]
    [ProducesResponseType(typeof(List<ScontrinoStatoViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetScontrinoStatoAsync()
    {
        try
        {
            var result = await utilityService.GetListScontrinoStatoAsync();
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpGet("TipoScontrino")]
    [ProducesResponseType(typeof(List<ScontrinoTipoViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTipoScontrinoAsync()
    {
        try
        {
            var result = await utilityService.GetListScontrinoTipoAsync();
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpGet("ValuteMonetarie")]
    [ProducesResponseType(typeof(List<ValutaViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetValuteAsync()
    {
        try
        {
            var result = await utilityService.GetListValuteAsync();
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }
}