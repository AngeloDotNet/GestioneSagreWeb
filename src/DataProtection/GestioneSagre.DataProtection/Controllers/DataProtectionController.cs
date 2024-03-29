﻿namespace GestioneSagre.DataProtection.Controllers;

public class DataProtectionController : BaseController
{
    private readonly IDataProtectionService dataProtectionCommands;

    public DataProtectionController(IDataProtectionService dataProtectionCommands)
    {
        this.dataProtectionCommands = dataProtectionCommands;
    }

    [HttpPost("data-protect")]
    [ProducesResponseType(typeof(ProtectionViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> DataProtectAsync(ProtectionInputModel inputModel)
    {
        var result = await dataProtectionCommands.EncryptMessage(inputModel);
        var response = HttpContext.CreateResponse(result);

        return response;
    }

    [HttpPost("data-unprotect")]
    [ProducesResponseType(typeof(ProtectionViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> DataUnprotectAsync(ProtectionInputModel inputModel)
    {
        var result = await dataProtectionCommands.DecryptMessage(inputModel);
        var response = HttpContext.CreateResponse(result);

        return response;
    }
}