using GestioneSagre.Menu.BusinessLayer.Services;
using GestioneSagre.Menu.Controllers.Common;
using GestioneSagre.Menu.Shared.Models.InputModels;
using GestioneSagre.Menu.Shared.Models.ViewModels;
using GestioneSagre.Messaging.Models.InputModels;
using GestioneSagre.Messaging.Models.ViewModels;
using GestioneSagre.Shared.OperationResults;
using GestioneSagre.Shared.OperationResults.WebApi;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.Menu.Controllers;

public class MenuController : BaseController
{
    private readonly IMenuService menuService;
    private readonly IRequestClient<RequestFestaAttiva> requestClient;
    private readonly ILogger<MenuController> logger;

    public MenuController(IMenuService menuService, IRequestClient<RequestFestaAttiva> requestClient, ILogger<MenuController> logger)
    {
        this.menuService = menuService;
        this.requestClient = requestClient;
        this.logger = logger;
    }

    [HttpGet("ListaMenu")]
    [ProducesResponseType(typeof(List<MenuViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetListaProdottiAsync()
    {
        try
        {
            var result = await menuService.GetListMenuAsync();
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpGet("RecoveryIdFesta")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRecoveryIdFestaAsync()
    {
        try
        {
            Result<string> idFesta = string.Empty;

            using (var request = requestClient.Create(new RequestFestaAttiva { }))
            {
                var response = await request.GetResponse<ResponseFestaAttiva>();

                if (response.Message.IdFesta == null)
                {
                    logger.LogWarning("Nessun riferimento di festa attiva trovato");
                    return HttpContext.CreateResponse(Result.Fail(FailureReasons.NotFound, "Nessun dato trovato", "Nessun riferimento di festa attiva trovato"));
                }

                idFesta = response.Message.IdFesta;
            }

            var result = HttpContext.CreateResponse(idFesta);

            return result;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpGet("Portata/{idMenu}/{idFesta}")]
    [ProducesResponseType(typeof(MenuViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProdottoAsync(Guid idMenu, Guid idFesta)
    {
        try
        {
            var result = await menuService.GetMenuAsync(idMenu, idFesta);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPost("Portata")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProdottoAsync(MenuCreateInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await menuService.CreateMenuAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpPut("Portata")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditProdottoAsync(MenuEditInputModel inputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await menuService.EditMenuAsync(inputModel, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }

    [HttpDelete("Portata/{idMenu}/{idFesta}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProdottoAsync(Guid idMenu, Guid idFesta, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await menuService.DeleteMenuAsync(idMenu, idFesta, cancellationToken);
            var response = HttpContext.CreateResponse(result);

            return response;
        }
        catch (Exception exc)
        {
            return HttpContext.CreateResponse(Result.Fail(FailureReasons.BadRequest, "Richiesta non valida", detail: exc.Message));
        }
    }
}