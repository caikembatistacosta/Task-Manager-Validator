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
                .ForPath(c => c.StatusDaDemanda,
                           x => x.MapFrom(src => src.StatusDemanda));
            CreateMap<Demanda, DemandaDetailsViewModel>()
                .ForPath(c => c.StatusDemanda,
                           x => x.MapFrom(src => src.StatusDaDemanda));
            CreateMap<Demanda, DemandaProgressViewModel>()
                .ForPath(c => c.StatusDemanda,
                           x => x.MapFrom(src => src.StatusDaDemanda));
            CreateMap<DemandaProgressViewModel, Demanda>()
                .ForPath(c => c.StatusDaDemanda,
                           x => x.MapFrom(src => src.StatusDemanda));
        }
    }
}
