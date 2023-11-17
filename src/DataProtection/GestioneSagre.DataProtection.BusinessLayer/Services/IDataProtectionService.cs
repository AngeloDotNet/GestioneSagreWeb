namespace GestioneSagre.DataProtection.BusinessLayer.Commands;

public interface IDataProtectionService
{
    Task<Result<ProtectionViewModel>> EncryptMessage(ProtectionInputModel inputModel);
    Task<Result<ProtectionViewModel>> DecryptMessage(ProtectionInputModel inputModel);
}