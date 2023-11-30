using AutoMapper;
using GestioneSagre.Categorie.DataAccessLayer.Entities;
using GestioneSagre.Categorie.Shared.Models.InputModels;
using GestioneSagre.Categorie.Shared.Models.ViewModels;

namespace GestioneSagre.Categorie.BusinessLayer.Mappers;

public class CategoriaMapperProfile : Profile
{
    public CategoriaMapperProfile()
    {
        CreateMap<Categoria, CategoriaCreateInputModel>().ReverseMap();
        CreateMap<Categoria, CategoriaEditInputModel>().ReverseMap();
        CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
    }
}
