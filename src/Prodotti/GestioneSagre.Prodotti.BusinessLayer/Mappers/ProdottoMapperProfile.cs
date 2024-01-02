using AutoMapper;
using GestioneSagre.Prodotti.DataAccessLayer.Entities;
using GestioneSagre.Prodotti.Shared.Models.InputModels;
using GestioneSagre.Prodotti.Shared.Models.ViewModels;

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