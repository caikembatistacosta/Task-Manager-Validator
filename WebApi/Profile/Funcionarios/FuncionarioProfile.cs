using Entities;
using WebApi.Models.Funcionario;

namespace WebApi.Profile.Funcionarios
{
    public class FuncionarioProfile : AutoMapper.Profile
    {
        public FuncionarioProfile()
        {

            CreateMap<FuncionariosInsertViewModel, Funcionario>()
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
            CreateMap<FuncionarioSelectViewModel, Funcionario>();
            CreateMap<Funcionario, FuncionarioSelectViewModel>()
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
            CreateMap<Funcionario, FuncionarioUpdateViewModel>();
            CreateMap<FuncionarioUpdateViewModel, Funcionario>()
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
                            x => x.MapFrom(src => src.Estado)); ;
            CreateMap<FuncionarioDetailsViewModel, Funcionario>();
            CreateMap<Funcionario, FuncionarioDetailsViewModel>()
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