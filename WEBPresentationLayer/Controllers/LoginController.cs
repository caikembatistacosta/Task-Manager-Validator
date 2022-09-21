using AutoMapper;
using BLL.Interfaces;
using Common;
using Common.Extensions;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WEBPresentationLayer.Models.Funcionario;

namespace WEBPresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly IFuncionarioService _funcionario;
        private readonly IMapper mapper;
        public LoginController(IFuncionarioService funcionario, IMapper mapper)
        {
            this._funcionario = funcionario;
            this.mapper = mapper;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logar(FuncionarioLoginViewModel funcionarioLogin)
        {
            funcionarioLogin.Senha = funcionarioLogin.Senha.Hash();
            Funcionario funcionario = mapper.Map<Funcionario>(funcionarioLogin);
            SingleResponse<Funcionario> singleResponse = await _funcionario.GetLogin(funcionario);
            if (!singleResponse.HasSuccess)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
