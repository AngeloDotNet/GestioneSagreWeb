using AutoMapper;
using GestioneSagre.Utility.DataAccessLayer.Entities;
using GestioneSagre.Utility.Shared.Models.ViewModels;

namespace GestioneSagre.Utility.BusinessLayer.Mappers;

public class PagamentoTipoMapperProfile : Profile
{
    public PagamentoTipoMapperProfile()
    {
        CreateMap<PagamentoTipo, PagamentoTipoViewModel>();
    }
}