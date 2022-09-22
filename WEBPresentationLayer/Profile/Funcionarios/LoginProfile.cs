using Entities;
using WEBPresentationLayer.Models.Funcionario;

namespace WEBPresentationLayer.Profile.Funcionarios
{
    public class LoginProfile : AutoMapper.Profile
    {
        public LoginProfile()
        {
            CreateMap<FuncionarioLoginViewModel, Funcionario>()
                 .ForPath(c => c.RefreshToken,
                          x => x.MapFrom(src => src.RefreshToken));
            CreateMap<Funcionario, FuncionarioLoginViewModel>();
        }
    }
}
