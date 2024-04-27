namespace GestioneSagre.Utility.BusinessLayer.Mappers;

public class ValutaMapperProfile : Profile
{
    public ValutaMapperProfile()
    {
        CreateMap<Valuta, ValutaViewModel>();
    }
}