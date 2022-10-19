using AutoMapper.Configuration.Conventions;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using WEBPresentationLayer.Models.Funcionario;

namespace WEBPresentationLayer.Controllers
{
    [Authorize]
    public class FuncionarioController : Controller
    {
        private readonly HttpClient _httpClient;
        public FuncionarioController(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://taskmanagervalidator.azurewebsites.net/");
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ClaimsPrincipal userLogado = this.User;
                string token = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage message = await _httpClient.GetAsync("Funcionario/All-Employeers");
                    if (message.IsSuccessStatusCode)
                    {
                        string json = await message.Content.ReadAsStringAsync();
                        List<FuncionarioSelectViewModel>? viewModels = JsonConvert.DeserializeObject<List<FuncionarioSelectViewModel>>(json);
                        if (viewModels == null)
                            return RedirectToAction("StatusCode", "Error");

                        return View(viewModels);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return RedirectToAction("StatusCode", "Error");
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FuncionariosInsertViewModel viewModel)
        {
            try
            {
                ClaimsPrincipal userLogado = this.User;
                string? token = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage message = await _httpClient.PostAsJsonAsync<FuncionariosInsertViewModel>("Funcionario/Employeer-Create", viewModel);
                    if (message.IsSuccessStatusCode)
                    {
                        string json = await message.Content.ReadAsStringAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return RedirectToAction("StatusCode", "Erorr");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                ClaimsPrincipal userLogado = this.User;
                string? token = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    HttpResponseMessage response = await _httpClient.GetAsync($"Funcionario/Employeer-Edit?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        FuncionarioUpdateViewModel? funcionario = JsonConvert.DeserializeObject<FuncionarioUpdateViewModel>(json);
                 
                        if (funcionario == null)
                            return RedirectToAction("StatusCode", "Error");

                        return View(funcionario);
                    }
                }

                return RedirectToAction("StatusCode", "Error");

            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FuncionarioUpdateViewModel viewModel)
        {
            try
            {
                var funcionarioDTO = new FuncionarioDTO()
                {
                    Bairro = viewModel.Endereco.Bairro,
                    CEP = viewModel.Endereco.Cep,
                    Cidade = viewModel.Endereco.Cidade,
                    Estado = viewModel.Endereco.Estado.UF,
                    Numero = viewModel.Endereco.Numero,
                    Rua = viewModel.Endereco.Rua,
                    DataNascimento = viewModel.DataNascimento,
                    Email = viewModel.Email,
                    Genero = viewModel.Genero,
                    Id = viewModel.Id,
                    NivelDeAcesso = viewModel.NivelDeAcesso,
                    Nome = viewModel.Nome,
                    Sobrenome = viewModel.Sobrenome
                };
                ClaimsPrincipal userLogado = this.User;
                string? token = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync<FuncionarioDTO>("Funcionario/Employeer-Edit", funcionarioDTO);
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string content = await httpResponseMessage.Content.ReadAsStringAsync();
                        return RedirectToAction(nameof(Index));

                    }
                    return RedirectToAction("StatusCode", "Error");
                }
                return RedirectToAction("StatusCode", "Error");

            }
            catch (Exception)
            {
                return RedirectToAction("StatusCode", "Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                ClaimsPrincipal userLogado = this.User;
                string? token = userLogado.Claims.FirstOrDefault(x => x?.Type == ClaimTypes.Sid).Value;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    HttpResponseMessage httpResponse = await _httpClient.GetAsync($"Funcionario/Employeer-Details?id={id}");
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        FuncionarioDetailsViewModel? funcionarioDetails = JsonConvert.DeserializeObject<FuncionarioDetailsViewModel>(content);
                        if (funcionarioDetails == null)
                        {
                            return RedirectToAction("StatusCode", "Error");
                        }
                        return View(funcionarioDetails);
                    }
                    return RedirectToAction("StatusCode", "Error");
                }
                return RedirectToAction("StatusCode", "Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("StatusCode", "Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> MyAcc()
        {
            try
            {
                ClaimsPrincipal userLogado = this.User;
                string? token = userLogado.Claims.FirstOrDefault(x => x?.Type == ClaimTypes.Sid).Value;
                string email = userLogado.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    HttpResponseMessage httpResponse = await _httpClient.GetAsync($"Funcionario/DetailsByEmail?email={email}");
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        FuncionarioDetailsViewModel? funcionarioDetails = JsonConvert.DeserializeObject<FuncionarioDetailsViewModel>(content);
                        if (funcionarioDetails == null)
                        {
                            return RedirectToAction("StatusCode", "Error");
                        }
                        return View(funcionarioDetails);
                    }
                    return RedirectToAction("StatusCode", "Error");
                }
                return RedirectToAction("StatusCode", "Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("StatusCode", "Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditSenha(int id)
        {
            try
            {
                ClaimsPrincipal userLogado = this.User;
                string? token = userLogado.Claims.FirstOrDefault(x => x?.Type == ClaimTypes.Sid).Value;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    HttpResponseMessage httpResponse = await _httpClient.GetAsync($"Funcionario/Edit-Password?id={id}");
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        FuncionarioUpdateSenhaViewModel? func = JsonConvert.DeserializeObject<FuncionarioUpdateSenhaViewModel>(content);
                        if (func == null)
                        {
                            return RedirectToAction("StatusCode", "Error");
                        }
                        return View(func);
                    }
                }
                return RedirectToAction("StatusCode", "Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("StatusCode", "Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditSenha(FuncionarioUpdateSenhaViewModel viewModel)
        {
            try
            {
                ClaimsPrincipal userLogado = this.User;
                string? token = userLogado.Claims.FirstOrDefault(x => x?.Type == ClaimTypes.Sid).Value;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync<FuncionarioUpdateSenhaViewModel>("Funcionario/Edit-Password", viewModel);
                    string content = await httpResponseMessage.Content.ReadAsStringAsync();
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else if (content.Contains("BadRequest"))
                    {
                        return View("SenhaErrada");
                    }
                    return RedirectToAction("StatusCode", "Error");
                }
                return RedirectToAction("StatusCode", "Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("StatusCode", "Error");
            }
        }

        public IActionResult Delete()
        {
            return View();
        }


    }
}