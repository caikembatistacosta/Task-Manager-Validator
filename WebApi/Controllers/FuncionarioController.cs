using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Shared;
using Shared.Extensions;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Funcionario;
using Microsoft.AspNetCore.Authorization;
using Entities.Enums;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    //[Authorize]
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioService _Funcionarios;
        private readonly IMapper _mapper;
        private readonly string? funcionarios;

        public FuncionarioController(IFuncionarioService svc, IMapper mapper)
        {
            this._Funcionarios = svc;
            this._mapper = mapper;
        }
        [HttpGet("All-Employeers")]
        public async Task<IActionResult> Index()
        {

            DataResponse<Funcionario> responseFuncionario = await _Funcionarios.GetAll();

            if (!responseFuncionario.HasSuccess)
            {
                return BadRequest(responseFuncionario.Message);
            }
            List<FuncionarioSelectViewModel> funcionario = _mapper.Map<List<FuncionarioSelectViewModel>>(responseFuncionario.Data);
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

            viewModel.Senha = viewModel.Senha.Hash();
            Funcionario Funcionario = _mapper.Map<Funcionario>(viewModel);

            Response response = await _Funcionarios.Insert(Funcionario);

            if (response.HasSuccess)
                return Ok(response);

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
            Funcionario funcionario = resoponseFuncionario.Item;
            FuncionarioUpdateViewModel updateViewModel = _mapper.Map<FuncionarioUpdateViewModel>(funcionario);
            return Ok(updateViewModel);

        }
        [HttpPut("Employeer-Edit")]
        public async Task<IActionResult> Edit(FuncionarioDTO viewModel)
        {

            Funcionario funcionarioUpdate = _Funcionarios.GetById(viewModel.Id).Result.Item;
            funcionarioUpdate.Nome = viewModel.Nome;
            funcionarioUpdate.Sobrenome = viewModel.Sobrenome;
            funcionarioUpdate.Email = viewModel.Email;
            funcionarioUpdate.DataNascimento = viewModel.DataNascimento;
            funcionarioUpdate.Genero = viewModel.Genero;
            funcionarioUpdate.NivelDeAcesso = viewModel.NivelDeAcesso;
            funcionarioUpdate.Endereco.Cep = viewModel.CEP;
            funcionarioUpdate.Endereco.Numero= viewModel.Numero;
            funcionarioUpdate.Endereco.Rua = viewModel.Rua;
            funcionarioUpdate.Endereco.Cidade = viewModel.Cidade;
            funcionarioUpdate.Endereco.Bairro = viewModel.Bairro;
            funcionarioUpdate.Endereco.Estado.UF = viewModel.Estado;

            Response response = await _Funcionarios.Update(funcionarioUpdate);
            if (!response.HasSuccess)
            {
                return BadRequest(response.Message);
            }
            return Ok(viewModel);
        }
        [HttpGet("Edit-Password")]
        public async Task<IActionResult> EditSenha(int id)
        {
            SingleResponse<Funcionario> resoponseFuncionario = await _Funcionarios.GetById(id);
            if (!resoponseFuncionario.HasSuccess)
            {
                return BadRequest(resoponseFuncionario.Message);
            }
            FuncionarioUpdateSenhaViewModel senhaViewModel = _mapper.Map<FuncionarioUpdateSenhaViewModel>(resoponseFuncionario.Item);
            if (senhaViewModel == null)
            {
                return BadRequest();
            }
            return Ok(senhaViewModel);
        }
        [HttpPut("Edit-Password")]
        public async Task<IActionResult> EditSenha(FuncionarioUpdateSenhaViewModel TrocarSenha)
        {
            TrocarSenha.Senha = TrocarSenha.Senha.Hash();
            //TrocarSenha.ID = id;
            Funcionario funcionario = _mapper.Map<Funcionario>(TrocarSenha);
            SingleResponse<Funcionario> funcionarioUpdate = await _Funcionarios.GetById(funcionario.ID);

            if (TrocarSenha.Senha.Equals(funcionarioUpdate.Item.Senha))
            {
                if (TrocarSenha.NovaSenha.Equals(TrocarSenha.NovaSenhaConfirmar))
                {
                    funcionarioUpdate.Item.Senha = TrocarSenha.NovaSenha.Hash();
                    funcionario.Senha = funcionarioUpdate.Item.Senha;
                    Response response = await _Funcionarios.Update(funcionarioUpdate.Item);
                    if (response.HasSuccess)
                    {
                        return Ok(response);
                    }
                    return BadRequest();
                }
                return BadRequest(funcionarioUpdate);
            }
            return BadRequest();
        }
        [HttpGet("Employeer-Details")]
        public async Task<IActionResult> Details(int id)
        {
            SingleResponse<Funcionario> single = await _Funcionarios.GetById(id);
            if (!single.HasSuccess)
            {
                return BadRequest(single.Message);
            }
            Funcionario funcionario = single.Item;
            FuncionarioDetailsViewModel viewModel = _mapper.Map<FuncionarioDetailsViewModel>(funcionario);
            return Ok(viewModel);
        }
        [HttpGet("Employeer-Delete")]
        public IActionResult Delete()
        {
            return Ok();
        }

    }
}