using AutoMapper;
using GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;
using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Intestazione;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;

namespace GestioneSagre.OperazioniAvvio.BusinessLayer.Mappers;

public class IntestazioneMapperProfile : Profile
{
    public IntestazioneMapperProfile()
    {
        CreateMap<Intestazione, IntestazioneCreateInputModel>().ReverseMap();
        CreateMap<Intestazione, IntestazioneEditInputModel>().ReverseMap();
        CreateMap<Intestazione, IntestazioneViewModel>().ReverseMap();
    }
}