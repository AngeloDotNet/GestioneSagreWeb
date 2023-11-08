using GestioneSagre.OperazioniAvvio.BusinessLayer.Services.Festa;
using GestioneSagre.OperazioniAvvio.Controllers.Common;
using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Festa;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;
using GestioneSagre.Shared.OperationResults.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.OperazioniAvvio.Controllers;

public class FesteController : BaseController
{
    private readonly IFesteService festeService;

    public FesteController(IFesteService festeService)
    {
        this.festeService = festeService;
    }

    [HttpGet("ListaFeste")]
    [ProducesResponseType(typeof(List<FestaViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetListaFesteAsync()
    {
        try
        {
            var result = await festeService.GetListFesteAsync();
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpGet("Festa/{idFesta}")]
    [ProducesResponseType(typeof(FestaViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProdottoAsync(Guid idFesta)
    {
        try
        {
            var result = await festeService.GetFestaAsync(idFesta);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPost("Festa")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateFestaAsync(FestaCreateInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await festeService.CreateFestaAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPut("Festa")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditFestaAsync(FestaEditInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await festeService.EditFestaAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    //[HttpDelete("Festa/{idFesta}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> DeleteProdottoAsync(Guid idFesta, CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        var result = await festeService.DeleteFestaAsync(idFesta, cancellationToken);
    //        var response = HttpContext.CreateResponse(result);

    //        return response;
    //    }
    //    catch (Exception exc)
    //    {
    //        return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
    //    }
    //}
}