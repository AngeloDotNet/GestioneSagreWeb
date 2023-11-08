using AutoMapper;
using GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;
using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Festa;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;

namespace GestioneSagre.OperazioniAvvio.BusinessLayer.Mappers;

public class FestaMapperProfile : Profile
{
    public FestaMapperProfile()
    {
        CreateMap<Festa, FestaCreateInputModel>().ReverseMap();
        CreateMap<Festa, FestaEditInputModel>().ReverseMap();
        CreateMap<Festa, FestaViewModel>().ReverseMap();
    }
}