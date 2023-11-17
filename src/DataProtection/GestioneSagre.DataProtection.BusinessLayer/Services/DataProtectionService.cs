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

    public Task<Result<ProtectionViewModel>> EncryptMessage(ProtectionInputModel inputModel)
    {
        logger.LogInformation("Inizio cifratura del messaggio");

        var protector = dataProtectionProvider.CreateProtector(keyProtector);
        var response = new ProtectionViewModel()
        {
            Message = protector.Protect(inputModel.Message)
        };

        logger.LogInformation("Fine crittografia del messaggio");

        var result = Task.FromResult<Result<ProtectionViewModel>>(response);
        return result;
    }

    public Task<Result<ProtectionViewModel>> DecryptMessage(ProtectionInputModel inputModel)
    {
        logger.LogInformation("Inizio decifratura del messaggio");

        var protector = dataProtectionProvider.CreateProtector(keyProtector);
        var response = new ProtectionViewModel()
        {
            Message = protector.Unprotect(inputModel.Message)
        };

        logger.LogInformation("Fine decifratura del messaggio");

        var result = Task.FromResult<Result<ProtectionViewModel>>(response);
        return result;
    }
}