using GestioneSagre.OperazioniAvvio.BusinessLayer.Services.Intestazione;
using GestioneSagre.OperazioniAvvio.Controllers.Common;
using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Intestazione;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;
using GestioneSagre.Shared.OperationResults.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.OperazioniAvvio.Controllers;

public class IntestazioniController : BaseController
{
    private readonly IIntestazioniService intestazioniService;

    public IntestazioniController(IIntestazioniService intestazioniService)
    {
        this.intestazioniService = intestazioniService;
    }

    [HttpGet("ListaIntestazioni")]
    [ProducesResponseType(typeof(List<IntestazioneViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetListaImpostazioniAsync()
    {
        try
        {
            var result = await intestazioniService.GetListIntestazioniAsync();
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpGet("Intestazione/{idFesta}")]
    [ProducesResponseType(typeof(IntestazioneViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetImpostazioneAsync(Guid idFesta)
    {
        try
        {
            var result = await intestazioniService.GetIntestazioneAsync(idFesta);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPost("Intestazione")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateImpostazioneAsync(IntestazioneCreateInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await intestazioniService.CreateIntestazioneAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPut("Intestazione")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditIntestazioneAsync(IntestazioneEditInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await intestazioniService.EditIntestazioneAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }
}
