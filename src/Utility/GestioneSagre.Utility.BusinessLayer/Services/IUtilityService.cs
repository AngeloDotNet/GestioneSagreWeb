namespace GestioneSagre.Utility.BusinessLayer.Services;

public interface IUtilityService
{
    Task<Result<List<ClienteTipoViewModel>>> GetListClienteTipoAsync();
    Task<Result<List<PagamentoTipoViewModel>>> GetListPagamentoTipoAsync();
    Task<Result<List<ScontrinoStatoViewModel>>> GetListScontrinoStatoAsync();
    Task<Result<List<ScontrinoTipoViewModel>>> GetListScontrinoTipoAsync();
    Task<Result<List<ValutaViewModel>>> GetListValuteAsync();
}