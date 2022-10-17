using Entities;
using WebApi.Models.Funcionario;

namespace WEBApi.Profile.Funcionarios
{
    public class LoginProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Mapeando os dados do funcionário.
        /// </summary>
        public LoginProfile()
        {
            CreateMap<FuncionarioLoginViewModel, Funcionario>();
            CreateMap<Funcionario, FuncionarioLoginViewModel>();
        }
    }
}
