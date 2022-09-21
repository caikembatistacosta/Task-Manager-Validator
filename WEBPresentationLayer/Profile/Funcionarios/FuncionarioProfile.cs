using Entities;
using WEBPresentationLayer.Models.Funcionario;

namespace WEBPresentationLayer.Profile.Funcionarios
{
    public class FuncionarioProfile : AutoMapper.Profile
    {
        public FuncionarioProfile()
        {
            
            CreateMap<FuncionariosInsertViewModel, Funcionario>();
            CreateMap<FuncionarioSelectViewModel, Funcionario>();
            CreateMap<Funcionario, FuncionarioSelectViewModel>();
            CreateMap<Funcionario, FuncionarioUpdateViewModel>();
            CreateMap<FuncionarioUpdateViewModel, Funcionario>();
            CreateMap<FuncionarioDetailsViewModel, Funcionario>();
            CreateMap<Funcionario, FuncionarioDetailsViewModel>();
        }
    }
}