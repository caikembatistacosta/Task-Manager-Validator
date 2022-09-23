using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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
            httpClient.BaseAddress = new Uri("https://localhost:7054/");
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
                ClaimsPrincipal userLogado = this.User;
                string? token = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                if (string.IsNullOrWhiteSpace(token))
                {
                    HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync<FuncionarioUpdateViewModel>("Funcionario/Employeer-Edit", viewModel);
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

        public IActionResult Delete()
        {
            return View();
        }

    }
}