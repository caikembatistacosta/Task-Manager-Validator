using Entities;
using WebApi.Models.Funcionario;

namespace WEBApi.Profile.Funcionarios
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
