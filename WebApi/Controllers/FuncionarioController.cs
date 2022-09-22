﻿using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Shared;
using Shared.Extensions;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Funcionario;

namespace WebApi.Controllers
{
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
        [HttpGet("All-Employeer")]
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
                return Ok(response.Message);

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
        [HttpPost("Employeer-Edit")]
        public async Task<IActionResult> Edit(FuncionarioUpdateViewModel viewModel)
        {
            viewModel.Senha = viewModel.Senha.Hash();
            Funcionario funcionario = _mapper.Map<Funcionario>(viewModel);
            Funcionario funcionarioUpdate = _Funcionarios.GetById(funcionario.ID).Result.Item;
            funcionarioUpdate.Senha = funcionario.Senha;
            funcionarioUpdate.Nome = funcionario.Nome;
            funcionarioUpdate.Sobrenome = funcionario.Sobrenome;
            funcionarioUpdate.DataNascimento = funcionario.DataNascimento;
            funcionarioUpdate.Genero = funcionario.Genero;
            funcionarioUpdate.NivelDeAcesso = funcionario.NivelDeAcesso;
            Response response = await _Funcionarios.Update(funcionarioUpdate);
            if (!response.HasSuccess)
            {
                return BadRequest(response.Message);
            }
            return Ok(funcionario);
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