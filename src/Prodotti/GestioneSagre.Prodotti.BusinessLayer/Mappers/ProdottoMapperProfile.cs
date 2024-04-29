namespace GestioneSagre.Prodotti.BusinessLayer.Mappers;

public class ProdottoMapperProfile : Profile
{
    public ProdottoMapperProfile()
    {
        CreateMap<Prodotto, ProdottoCreateInputModel>().ReverseMap();
        CreateMap<Prodotto, ProdottoEditInputModel>().ReverseMap();
        CreateMap<Prodotto, ProdottoViewModel>().ReverseMap();
    }
}