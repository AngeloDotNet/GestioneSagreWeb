namespace GestioneSagre.Utility.BusinessLayer.Mappers;

public class PagamentoTipoMapperProfile : Profile
{
    public PagamentoTipoMapperProfile()
    {
        CreateMap<PagamentoTipo, PagamentoTipoViewModel>();
    }
}