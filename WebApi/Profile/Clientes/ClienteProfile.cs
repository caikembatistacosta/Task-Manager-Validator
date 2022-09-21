using Entities;
using WebApi.Models.Cliente;

namespace WEBApi.Profile.Clientes
{
    public class ClientesProfile : AutoMapper.Profile
    {
        public ClientesProfile()
        {
            CreateMap<ClienteSelectViewModel, Cliente>();
            CreateMap<Cliente, ClienteSelectViewModel>()
                    .ForPath(c => c.Endereco.Cep,
                          x => x.MapFrom(src => src.Endereco.Cep))
                .ForPath(c => c.Endereco.Numero,
                          x => x.MapFrom(src => src.Endereco.Numero))
                .ForPath(c => c.Endereco.Bairro,
                          x => x.MapFrom(src => src.Endereco.Bairro))
                .ForPath(c => c.Endereco.Cidade,
                          x => x.MapFrom(src => src.Endereco.Cidade))
                .ForPath(c => c.Endereco.Rua,
                          x => x.MapFrom(src => src.Endereco.Rua))
                .ForPath(c => c.Endereco.Estado.UF,
                            x => x.MapFrom(src => src.Endereco.Estado.UF));
            
            CreateMap<Cliente, ClienteUpdateViewModel>();
            CreateMap<ClienteUpdateViewModel, Cliente>()
                .ForPath(c => c.Endereco.Cep,
                          x => x.MapFrom(src => src.CEP))
                .ForPath(c => c.Endereco.Numero,
                          x => x.MapFrom(src => src.Numero))
                .ForPath(c => c.Endereco.Bairro,
                          x => x.MapFrom(src => src.Bairro))
                .ForPath(c => c.Endereco.Cidade,
                          x => x.MapFrom(src => src.Cidade))
                .ForPath(c => c.Endereco.Rua,
                          x => x.MapFrom(src => src.Rua))
                .ForPath(c => c.Endereco.Estado.UF,
                            x => x.MapFrom(src => src.Estado));
            
            CreateMap<Cliente, ClienteInsertViewModel>();
            CreateMap<ClienteInsertViewModel, Cliente>()
                .ForPath(c => c.Endereco.Cep,
                          x => x.MapFrom(src => src.CEP))
                .ForPath(c => c.Endereco.Numero,
                          x => x.MapFrom(src => src.Numero))
                .ForPath(c => c.Endereco.Bairro,
                          x => x.MapFrom(src => src.Bairro))
                .ForPath(c => c.Endereco.Cidade,
                          x => x.MapFrom(src => src.Cidade))
                .ForPath(c => c.Endereco.Rua,
                          x => x.MapFrom(src => src.Rua))
                .ForPath(c => c.Endereco.Estado.UF,
                            x => x.MapFrom(src => src.Estado));

            CreateMap<ClienteDetailsViewModel, Cliente>();
            CreateMap<Cliente, ClienteDetailsViewModel>()
                .ForPath(c => c.Endereco.Cep,
                          x => x.MapFrom(src => src.Endereco.Cep))
                .ForPath(c => c.Endereco.Numero,
                          x => x.MapFrom(src => src.Endereco.Numero))
                .ForPath(c => c.Endereco.Bairro,
                          x => x.MapFrom(src => src.Endereco.Bairro))
                .ForPath(c => c.Endereco.Cidade,
                          x => x.MapFrom(src => src.Endereco.Cidade))
                .ForPath(c => c.Endereco.Rua,
                          x => x.MapFrom(src => src.Endereco.Rua))
                .ForPath(c => c.Endereco.Estado.UF,
                            x => x.MapFrom(src => src.Endereco.Estado.UF));

        }
    }
}
