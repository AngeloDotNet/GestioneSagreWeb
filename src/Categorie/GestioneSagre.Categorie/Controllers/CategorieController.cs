using GestioneSagre.Categorie.BusinessLayer.Services;
using GestioneSagre.Categorie.Controllers.Common;
using GestioneSagre.Categorie.Shared.Models.InputModels;
using GestioneSagre.Categorie.Shared.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;
using GestioneSagre.Shared.OperationResults.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.Categorie.Controllers;

public class CategorieController : BaseController
{
    private readonly ICategorieService categorieService;

    public CategorieController(ICategorieService categorieService)
    {
        this.categorieService = categorieService;
    }

    [HttpGet("ListaCategorie")]
    [ProducesResponseType(typeof(List<CategoriaViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetListaCategorieAsync()
    {
        try
        {
            var result = await categorieService.GetListCategorieAsync();
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpGet("Categoria/{idCategoria}/{idFesta}")]
    [ProducesResponseType(typeof(CategoriaViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCategoriaAsync(Guid idCategoria, Guid idFesta)
    {
        try
        {
            var result = await categorieService.GetCategoriaAsync(idCategoria, idFesta);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPost("Categoria")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategoriaAsync(CategoriaCreateInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await categorieService.CreateCategoriaAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPut("Categoria")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditCategoriaAsync(CategoriaEditInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await categorieService.EditCategoriaAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpDelete("Categoria/{idCategoria}/{idFesta}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCategoriaAsync(Guid idCategoria, Guid idFesta, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await categorieService.DeleteCategoriaAsync(idCategoria, idFesta, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }
}
