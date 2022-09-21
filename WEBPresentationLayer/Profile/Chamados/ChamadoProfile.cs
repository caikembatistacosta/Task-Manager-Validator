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
        }
    }
}
