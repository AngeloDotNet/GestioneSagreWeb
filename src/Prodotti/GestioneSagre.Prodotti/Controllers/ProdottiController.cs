using GestioneSagre.Prodotti.BusinessLayer.Services;
using GestioneSagre.Prodotti.Controllers.Common;
using GestioneSagre.Prodotti.Shared.Models.InputModels;
using GestioneSagre.Prodotti.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;
using GestioneSagre.Shared.OperationResults.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.Prodotti.Controllers;

public class ProdottiController : BaseController
{
    private readonly IProdottiService prodottiService;

    public ProdottiController(IProdottiService prodottiService)
    {
        this.prodottiService = prodottiService;
    }

    [HttpGet("ListaProdotti")]
    [ProducesResponseType(typeof(List<ProdottoViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetListaProdottiAsync()
    {
        try
        {
            var result = await prodottiService.GetListProdottiAsync();
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpGet("Prodotto/{idProdotto}/{idFesta}")]
    [ProducesResponseType(typeof(ProdottoViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProdottoAsync(Guid idProdotto, Guid idFesta)
    {
        try
        {
            var result = await prodottiService.GetProdottoAsync(idProdotto, idFesta);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPost("Prodotto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProdottoAsync(ProdottoCreateInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await prodottiService.CreateProdottoAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPut("Prodotto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditProdottoAsync(ProdottoEditInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await prodottiService.EditProdottoAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpDelete("Prodotto/{idProdotto}/{idFesta}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProdottoAsync(Guid idProdotto, Guid idFesta, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await prodottiService.DeleteProdottoAsync(idProdotto, idFesta, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }
}