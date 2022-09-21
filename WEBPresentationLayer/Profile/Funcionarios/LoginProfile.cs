using Entities;
using WEBPresentationLayer.Models.Funcionario;

namespace WEBPresentationLayer.Profile.Funcionarios
{
    public class LoginProfile : AutoMapper.Profile
    {
        public LoginProfile()
        {
            CreateMap<FuncionarioLoginViewModel, Funcionario>();
            CreateMap<Funcionario, FuncionarioLoginViewModel>();
        }
    }
}
