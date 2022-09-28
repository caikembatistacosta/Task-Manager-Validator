using Entities;
using WebApi.Models.Demanda;

namespace WebApi.Profile.Demandas
{
    public class DemandaProfile : AutoMapper.Profile
    {
        public DemandaProfile()
        {
            CreateMap<DemandaInsertViewModel, Demanda>();
            CreateMap<DemandaSelectViewModel, Demanda>();
            CreateMap<DemandaUpdateViewModel, Demanda>();
            CreateMap<Demanda, DemandaUpdateViewModel>();
            CreateMap<Demanda, DemandaSelectViewModel>();
            CreateMap<DemandaDetailsViewModel, Demanda>()
                .ForPath(c => c.ID,
                           x => x.MapFrom(src => src.ID))
                .ForPath(c => c.DataFim,
                           x => x.MapFrom(src => src.DataFim))
                .ForPath(c => c.DataInicio,
                           x => x.MapFrom(src => src.DataInicio))
                .ForPath(c => c.Nome,
                           x => x.MapFrom(src => src.Nome))
                .ForPath(c => c.DescricaoCurta,
                           x => x.MapFrom(src => src.DescricaoCurta))
                .ForPath(c => c.DescricaoDetalhada,
                           x => x.MapFrom(src => src.DescricaoDetalhada))
                .ForPath(c => c.StatusDaDemanda,
                           x => x.MapFrom(src => src.StatusDemanda));
            CreateMap<Demanda, DemandaDetailsViewModel>()
                                .ForPath(c => c.ID,
                           x => x.MapFrom(src => src.ID))
                .ForPath(c => c.DataFim,
                           x => x.MapFrom(src => src.DataFim))
                .ForPath(c => c.DataInicio,
                           x => x.MapFrom(src => src.DataInicio))
                .ForPath(c => c.Nome,
                           x => x.MapFrom(src => src.Nome))
                .ForPath(c => c.DescricaoCurta,
                           x => x.MapFrom(src => src.DescricaoCurta))
                .ForPath(c => c.DescricaoDetalhada,
                           x => x.MapFrom(src => src.DescricaoDetalhada))
                .ForPath(c => c.StatusDemanda,
                           x => x.MapFrom(src => src.StatusDaDemanda));
        }
    }
}
