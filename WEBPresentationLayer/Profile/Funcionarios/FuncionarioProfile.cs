using Entities;
using WEBPresentationLayer.Models.Funcionario;

namespace WEBPresentationLayer.Profile.Funcionarios
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
            CreateMap<Funcionario, FuncionarioUpdateViewModel>().ForPath(c => c.CEP,
                          x => x.MapFrom(src => src.Endereco.Cep))
                .ForPath(c => c.Numero,
                          x => x.MapFrom(src => src.Endereco.Numero))
                .ForPath(c => c.Bairro,
                          x => x.MapFrom(src => src.Endereco.Bairro))
                .ForPath(c => c.Cidade,
                          x => x.MapFrom(src => src.Endereco.Cidade))
                .ForPath(c => c.Rua, 
                          x => x.MapFrom(src => src.Endereco.Rua))
                .ForPath(c => c.Estado,
                            x => x.MapFrom(src => src.Endereco.Estado.UF));
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
                            x => x.MapFrom(src => src.Estado));
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
            CreateMap<FuncionarioUpdateSenhaViewModel, Funcionario>()
                .ForPath(c => c.ID,
                         x => x.MapFrom(src => src.ID));
            CreateMap<Funcionario, FuncionarioUpdateSenhaViewModel>();
        }
    }
}