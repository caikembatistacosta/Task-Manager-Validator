using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Shared;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models.Funcionario;
using WebApi.Models.Token;
using Shared.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IFuncionarioService _funcionario;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        public LoginController(IFuncionarioService funcionario, IMapper mapper, ITokenService service)
        {
            _funcionario = funcionario;
            this.mapper = mapper;
            tokenService = service;
        }
        [HttpGet("Login")]
        public IActionResult Index()
        {
            return Ok();
        }
        [HttpPost("Logar")]
        public async Task<IActionResult> Logar([FromBody] FuncionarioLoginViewModel funcionarioLogin)
        {
            Funcionario funcionario = mapper.Map<Funcionario>(funcionarioLogin);
            
            funcionario.Senha = funcionario.Senha.Hash();
            SingleResponse<Funcionario> singleResponse = await _funcionario.GetLogin(funcionario);
            if (!singleResponse.HasSuccess)
            {
                return BadRequest(singleResponse.Message);
            }
            SingleResponse<string> token = tokenService.GenerateToken(singleResponse.Item);
            SingleResponse<string> refreshToken = tokenService.RefreshToken();
            SingleResponse<Funcionario> response = await tokenService.InsertRefreshToken(funcionario.Email, refreshToken.Item);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }
            return Ok(new FuncionarioLoginViewModel
            {
                Email = response.Item.Email,
                Senha = response.Item.Senha,
                Token = token.Item,
                RefreshToken = response.Item.RefreshToken
            });
        }
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenViewModel viewModel)
        {
            SingleResponse<ClaimsPrincipal> principal = tokenService.GetPrincipalFromExpiredToken(viewModel.Token);
            string email = principal.Item.Identity.Name;
            SingleResponse<Funcionario> savedRefreshToken = await tokenService.GetRefreshToken(email);
            if (savedRefreshToken.Item.RefreshToken.Equals(viewModel.RefreshToken))
            {
                SingleResponse<string> newJwtToken = tokenService.GenerateToken(principal.Item.Claims);
                if (newJwtToken.HasSuccess)
                {
                    SingleResponse<string> newRefreshToken = tokenService.RefreshToken();
                    if (newRefreshToken.HasSuccess)
                    {
                        Response response = await tokenService.DeleteRefreshToken(email, newRefreshToken.Item);
                        if (response.HasSuccess)
                        {
                            SingleResponse<Funcionario> newRToken = await tokenService.InsertRefreshToken(email, newRefreshToken.Item);
                            if (newRToken.HasSuccess)
                            {
                                return Ok(new AuthenticateResponse
                                {
                                    Token = newJwtToken.Item,
                                    Refresh = newRToken.Item.RefreshToken,
                                });
                            }
                            return BadRequest(newRToken.Message);
                        }
                        return BadRequest(response.Message);
                    }
                    return BadRequest(newRefreshToken.Message);
                }
                return BadRequest(newJwtToken.Message);
            }
            return BadRequest(savedRefreshToken.Message);
        }
    }
}
