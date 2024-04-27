namespace GestioneSagre.Utility.BusinessLayer.Mappers;

public class ClienteTipoMapperProfile : Profile
{
    public ClienteTipoMapperProfile()
    {
        CreateMap<ClienteTipo, ClienteTipoViewModel>();
    }
}