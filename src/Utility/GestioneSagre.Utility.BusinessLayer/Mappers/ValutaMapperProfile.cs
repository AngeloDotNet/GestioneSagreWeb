using AutoMapper;
using GestioneSagre.Utility.DataAccessLayer.Entities;
using GestioneSagre.Utility.Shared.Models.ViewModels;

namespace GestioneSagre.Utility.BusinessLayer.Mappers;

public class ValutaMapperProfile : Profile
{
    public ValutaMapperProfile()
    {
        CreateMap<Valuta, ValutaViewModel>();
    }
}