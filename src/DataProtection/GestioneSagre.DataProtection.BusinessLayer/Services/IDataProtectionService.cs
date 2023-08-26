namespace GestioneSagre.DataProtection.BusinessLayer.Commands;

public interface IDataProtectionService
{
    Task<ProtectionViewModel> EncryptMessage(ProtectionInputModel inputModel);
    Task<ProtectionViewModel> DecryptMessage(ProtectionInputModel inputModel);
}