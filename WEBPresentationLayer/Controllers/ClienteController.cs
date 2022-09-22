using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using WEBPresentationLayer.Models.Cliente;

namespace WEBPresentationLayer.Controllers
{
    [Authorize(Policy = "RequireADM")]
    public class ClienteController : Controller
    {
        private readonly HttpClient _httpClient;
        public ClienteController(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7202/");
            _httpClient = httpClient;
           
        }
        
        public async Task<IActionResult> Index()
        {
            try
            {
                ClaimsPrincipal userLogado = this.User;
                string token = userLogado.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage message = await _httpClient.GetAsync("Cliente/All-Costumers");
                if (message.IsSuccessStatusCode)
                {
                    string json = await message.Content.ReadAsStringAsync();
                    List<ClienteSelectViewModel>? cliente = JsonConvert.DeserializeObject<List<ClienteSelectViewModel>>(json);
                    if (cliente == null)
                        return NotFound();
                    return View(cliente);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteInsertViewModel viewModel)
        {
            HttpResponseMessage message = await _httpClient.PostAsJsonAsync<ClienteInsertViewModel>("Cliente/Insert-Costumer", viewModel);
            if (message.IsSuccessStatusCode)
            {
                string content = await message.Content.ReadAsStringAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Cliente/Edit-Costumer?id={id}");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                ClienteUpdateViewModel? cliente = JsonConvert.DeserializeObject<ClienteUpdateViewModel>(json);
                if (cliente == null)
                    return NotFound();
                return View(cliente);

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ClienteUpdateViewModel viewModel)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync<ClienteUpdateViewModel>("Cliente/Update-Costumer", viewModel);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string content = await httpResponseMessage.Content.ReadAsStringAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"Cliente/Costumer-Details?id={id}");
                httpResponseMessage.EnsureSuccessStatusCode();
                string json = await httpResponseMessage.Content.ReadAsStringAsync();
                ClienteDetailsViewModel? demandaDetailsViewModel = JsonConvert.DeserializeObject<ClienteDetailsViewModel>(json);
                if (demandaDetailsViewModel == null)
                {
                    return NotFound();
                }
                return View(demandaDetailsViewModel);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
