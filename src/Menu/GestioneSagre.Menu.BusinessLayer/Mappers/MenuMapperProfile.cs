using AutoMapper;
using GestioneSagre.Menu.DataAccessLayer.Entities;
using GestioneSagre.Menu.Shared.Models.InputModels;
using GestioneSagre.Menu.Shared.Models.ViewModels;

namespace GestioneSagre.Menu.BusinessLayer.Mappers;

public class MenuMapperProfile : Profile
{
    public MenuMapperProfile()
    {
        CreateMap<Portata, MenuCreateInputModel>().ReverseMap();
        CreateMap<Portata, MenuEditInputModel>().ReverseMap();
        CreateMap<Portata, MenuViewModel>().ReverseMap();
    }
}