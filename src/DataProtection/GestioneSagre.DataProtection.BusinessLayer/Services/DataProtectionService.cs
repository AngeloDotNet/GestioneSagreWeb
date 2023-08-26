namespace GestioneSagre.DataProtection.BusinessLayer.Commands;

public class DataProtectionService : IDataProtectionService
{
    private readonly ILogger<DataProtectionService> logger;
    private readonly IDataProtectionProvider dataProtectionProvider;
    private readonly string keyProtector = "default";

    public DataProtectionService(ILogger<DataProtectionService> logger, IDataProtectionProvider dataProtectionProvider)
    {
        this.logger = logger;
        this.dataProtectionProvider = dataProtectionProvider;
    }

    public Task<ProtectionViewModel> EncryptMessage(ProtectionInputModel inputModel)
    {
        logger.LogInformation("Encrypting message");

        var protector = dataProtectionProvider.CreateProtector(keyProtector);
        var response = new ProtectionViewModel()
        {
            Message = protector.Protect(inputModel.Message)
        };

        return Task.FromResult(response);
    }

    public Task<ProtectionViewModel> DecryptMessage(ProtectionInputModel inputModel)
    {
        logger.LogInformation("Decrypting message");

        var protector = dataProtectionProvider.CreateProtector(keyProtector);
        var response = new ProtectionViewModel()
        {
            Message = protector.Unprotect(inputModel.Message)
        };

        return Task.FromResult(response);
    }
}