using GestioneSagre.DataProtection.BusinessLayer.Commands;
using GestioneSagre.DataProtection.Controllers.Common;
using GestioneSagre.DataProtection.Shared.Models.InputModels;
using GestioneSagre.DataProtection.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.DataProtection.Controllers;

public class DataProtectionController : BaseController
{
    private readonly IDataProtectionService dataProtectionCommands;

    public DataProtectionController(IDataProtectionService dataProtectionCommands)
    {
        this.dataProtectionCommands = dataProtectionCommands;
    }

    [HttpPost("data-protect")]
    public Task<ProtectionViewModel> DataProtect(ProtectionInputModel inputModel)
        => dataProtectionCommands.EncryptMessage(inputModel);

    [HttpPost("data-unprotect")]
    public Task<ProtectionViewModel> DataUnprotect(ProtectionInputModel inputModel)
        => dataProtectionCommands.DecryptMessage(inputModel);
}