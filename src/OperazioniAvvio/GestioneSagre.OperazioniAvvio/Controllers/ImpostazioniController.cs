using GestioneSagre.OperazioniAvvio.BusinessLayer.Services.Impostazione;
using GestioneSagre.OperazioniAvvio.Controllers.Common;
using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Impostazione;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;
using GestioneSagre.Shared.OperationResults.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.OperazioniAvvio.Controllers;

public class ImpostazioniController : BaseController
{
    private readonly IImpostazioniService impostazioniService;

    public ImpostazioniController(IImpostazioniService impostazioniService)
    {
        this.impostazioniService = impostazioniService;
    }

    [HttpGet("ListaImpostazioni")]
    [ProducesResponseType(typeof(List<ImpostazioneViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetListaImpostazioniAsync()
    {
        try
        {
            var result = await impostazioniService.GetListImpostazioniAsync();
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpGet("Impostazione/{idFesta}")]
    [ProducesResponseType(typeof(ImpostazioneViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetImpostazioneAsync(Guid idFesta)
    {
        try
        {
            var result = await impostazioniService.GetImpostazioneAsync(idFesta);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPost("Impostazione")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateImpostazioneAsync(ImpostazioneCreateInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await impostazioniService.CreateImpostazioneAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPut("Impostazione")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditImpostazioneAsync(ImpostazioneEditInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await impostazioniService.EditImpostazioneAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }
}
