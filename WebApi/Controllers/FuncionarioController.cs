using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Shared;
using Shared.Extensions;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Funcionario;
using Microsoft.AspNetCore.Authorization;
using log4net;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    [Authorize(Policy ="RequireAdm")]
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioService _Funcionarios;
        private readonly IMapper _mapper;
        private readonly ILog log;
        public FuncionarioController(IFuncionarioService svc, IMapper mapper, ILog log)
        {
            this._Funcionarios = svc;
            this._mapper = mapper;
            this.log = log;
        }
        [HttpGet("All-Employeers")]
        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            log.Debug($"O usuário {user} está acessando todos os funcionários");
            DataResponse<Funcionario> responseFuncionario = await _Funcionarios.GetAll();

            if (!responseFuncionario.HasSuccess)
            {
                log.Warn($"{user} Não obteve sucesso ao pegar todos os funcionários");
                return BadRequest(responseFuncionario.Message);
            }
            List<FuncionarioSelectViewModel> funcionario = _mapper.Map<List<FuncionarioSelectViewModel>>(responseFuncionario.Data);
            log.Info($"{user} Obteve sucesso ao pegar todos os funcionários");
            return Ok(funcionario);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost("Employeer-Create")]
        public async Task<IActionResult> Create(FuncionariosInsertViewModel viewModel)
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            log.Debug($"O {user} está cadastrando um funcionário");
            viewModel.Senha = viewModel.Senha.Hash();
            Funcionario Funcionario = _mapper.Map<Funcionario>(viewModel);

            Response response = await _Funcionarios.Insert(Funcionario);

            if (response.HasSuccess)
            {
                log.Info($"O {user} obteve sucesso ao cadastrar um novo usuário");
                return Ok(response);
            }
            log.Warn($"O {user} não obteve sucesso para cadastrar um novo usuário");
            return BadRequest(response.Message);
        }
        [HttpGet("Employeer-Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            SingleResponse<Funcionario> resoponseFuncionario = await _Funcionarios.GetById(id);
            if (!resoponseFuncionario.HasSuccess)
            {
                return BadRequest(resoponseFuncionario.Message);
            }
            FuncionarioUpdateViewModel updateViewModel = _mapper.Map<FuncionarioUpdateViewModel>(resoponseFuncionario.Item);
            return Ok(updateViewModel);

        }
        [HttpPut("Employeer-Edit")]
        public async Task<IActionResult> Edit(FuncionarioUpdateViewModel viewModel)
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            log.Debug($"O {user} está tentando atualizar os dados de um funcionário");
            Funcionario funcionario = _mapper.Map<Funcionario>(viewModel);
            Response response = await _Funcionarios.Update(funcionario);
            if (!response.HasSuccess)
            {
                log.Warn($"O {user} não obteve sucesso na atualização");
                return BadRequest(response.Message);
            }
            log.Info($"O {user} obteve sucesso na atualização dos dados");
            return Ok(viewModel);
        }
        [HttpGet("Edit-Password")]
        public async Task<IActionResult> EditSenha(int id)
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            log.Debug($"O usuário {user} tentará trocar sua senha");
            SingleResponse<Funcionario> resoponseFuncionario = await _Funcionarios.GetById(id);
            if (!resoponseFuncionario.HasSuccess)
            {
                log.Warn($"O usário não foi encontrado por {user}");
                return BadRequest(resoponseFuncionario.Message);
            }
            FuncionarioUpdateSenhaViewModel senhaViewModel = _mapper.Map<FuncionarioUpdateSenhaViewModel>(resoponseFuncionario.Item);
            log.Info($"Sucesso ao pegar o usuário por {user}");
            return Ok(senhaViewModel);
        }
        [HttpPut("Edit-Password")]
        public async Task<IActionResult> EditSenha(FuncionarioUpdateSenhaViewModel TrocarSenha)
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            log.Debug($"O usuário {user} está efetuando a troca de senha");
            TrocarSenha.Senha = TrocarSenha.Senha.Hash();
            Funcionario funcionario = _mapper.Map<Funcionario>(TrocarSenha);
            SingleResponse<Funcionario> funcionarioUpdate = await _Funcionarios.GetById(funcionario.ID);

            if (TrocarSenha.Senha.Equals(funcionarioUpdate.Item.Senha))
            {
                log.Info("Sucesso ao achar a senha antiga");
                if (TrocarSenha.NovaSenha.Equals(TrocarSenha.NovaSenhaConfirmar))
                {
                    log.Info("Sucesso ao verificar se as senhas são iguais");
                    funcionarioUpdate.Item.Senha = TrocarSenha.NovaSenha.Hash();
                    funcionario.Senha = funcionarioUpdate.Item.Senha;
                    Response response = await _Funcionarios.Update(funcionarioUpdate.Item);
                    if (response.HasSuccess)
                    {
                        log.Info($"O Usuário {user} atualizou a senha com sucesso");
                        return Ok(response);
                    }
                    log.Warn($"O usuário {user} não obteve sucesso ao trocar as senhas");
                    return BadRequest();
                }
                log.Warn($"O usuário {user} não colocou as senhas iguais");
                return BadRequest(funcionarioUpdate);
            }
            log.Warn($"O usuário {user} não colocou as senha igual a antiga");
            return BadRequest();
        }
        [HttpGet("Employeer-Details")]
        public async Task<IActionResult> Details(int id)
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            SingleResponse<Funcionario> single = await _Funcionarios.GetById(id);
            log.Debug($"O usuario {user} está tentando acessar os detalhes do funcionario {single.Item.ID}");
            if (!single.HasSuccess)
            {
                log.Warn($"O usuário {user} não obteve sucesso ao pegar os dados desse funcionário");
                return BadRequest(single.Message);
            }
            FuncionarioDetailsViewModel viewModel = _mapper.Map<FuncionarioDetailsViewModel>(single.Item);
            log.Info($"O usuário {user} obteve sucesso ao pegar os dados do funcionário");
            return Ok(viewModel);
        }
        [HttpGet("DetailsByEmail")]
        public async Task<IActionResult> DetailsByEmail(string email)
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            log.Debug($"O usuario {user} está tentando acessar sua própria conta.");
            SingleResponse<Funcionario> single = await _Funcionarios.GetByEmail(email);
            if (!single.HasSuccess)
            {
                log.Warn($"O usuário {user} não obteve sucesso ao pegar os próprios dados.");
                return BadRequest(single.Message);
            }
            FuncionarioDetailsViewModel viewModel = _mapper.Map<FuncionarioDetailsViewModel>(single.Item);
            log.Info($"O usuário {user} obteve sucesso ao pegar os próprios dados.");
            return Ok(viewModel);
        }


    }
}