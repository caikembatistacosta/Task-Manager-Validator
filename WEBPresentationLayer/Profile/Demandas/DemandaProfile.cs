using Entities;
using WEBPresentationLayer.Models.Demanda;

namespace WEBPresentationLayer.Profile.Demandas
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
            CreateMap<DemandaDetailsViewModel, Demanda>();
            CreateMap<Demanda, DemandaDetailsViewModel>();
            CreateMap<Demanda, DemandaProgressViewModel>()
                .ForPath(x => x.StatusDaDemanda,
                        src => src.MapFrom(x => x.StatusDaDemanda)); ;
            CreateMap<DemandaProgressViewModel, Demanda>()
                .ForPath(x => x.StatusDaDemanda, 
                        src => src.MapFrom(x => x.StatusDaDemanda));
        }
    }
}
