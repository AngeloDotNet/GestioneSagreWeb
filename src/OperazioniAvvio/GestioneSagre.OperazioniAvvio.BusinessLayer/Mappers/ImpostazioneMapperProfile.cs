using AutoMapper;
using GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;
using GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Impostazione;
using GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;

namespace GestioneSagre.OperazioniAvvio.BusinessLayer.Mappers;

public class ImpostazioneMapperProfile : Profile
{
    public ImpostazioneMapperProfile()
    {
        CreateMap<Impostazione, ImpostazioneCreateInputModel>().ReverseMap();
        CreateMap<Impostazione, ImpostazioneEditInputModel>().ReverseMap();
        CreateMap<Impostazione, ImpostazioneViewModel>().ReverseMap();
    }
}