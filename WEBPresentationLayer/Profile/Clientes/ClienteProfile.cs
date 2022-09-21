using Entities;
using WEBPresentationLayer.Models.Cliente;

namespace WEBPresentationLayer.Profile.Clientes
{
    public class DemandasProfile : AutoMapper.Profile
    {
        public DemandasProfile()
        {
            CreateMap<ClienteInsertViewModel, Cliente>();
            CreateMap<ClienteSelectViewModel, Cliente>();
            CreateMap<Cliente, ClienteSelectViewModel>();
            CreateMap<Cliente, ClienteUpdateViewModel> ();
            CreateMap<ClienteUpdateViewModel, Cliente> ();
        }
    }
}
