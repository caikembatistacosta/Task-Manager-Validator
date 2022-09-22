using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using WEBPresentationLayer.Models.Demanda;

namespace WEBPresentationLayer.Controllers
{
    [Authorize(Policy = "RequireAdm")]
    public class DemandaController : Controller
    {
        private readonly HttpClient _httpClient;
        public DemandaController(HttpClient httpClient)
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
                HttpResponseMessage response = await _httpClient.GetAsync("Demanda/All-Demands");
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                List<DemandaSelectViewModel>? chamado = JsonConvert.DeserializeObject<List<DemandaSelectViewModel>>(json);
                if (chamado == null)
                {
                    return NotFound();
                }
                return View(chamado);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
           
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DemandaInsertViewModel viewModel)
        {
            try
            {
                ClaimsPrincipal userLogado = this.User;
                string token = userLogado.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage message = await _httpClient.PostAsJsonAsync<DemandaInsertViewModel>("Demanda/Insert-Demands", viewModel);

                if (!message.IsSuccessStatusCode)
                    return NotFound();

                string content = await message.Content.ReadAsStringAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"Demanda/Edit-Demands?id={id}");
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                DemandaUpdateViewModel? chamado = JsonConvert.DeserializeObject<DemandaUpdateViewModel>(json);
                if (chamado == null)
                    return NotFound();

                return View(chamado);
            }
            catch (Exception)
            {
                return NotFound();
            }
            

        }
        [HttpPost]
        public async Task<IActionResult> Edit(DemandaUpdateViewModel viewModel)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync<DemandaUpdateViewModel>("Demanda/Edit-Demands",viewModel);
                string content = await httpResponseMessage.Content.ReadAsStringAsync();
                if (content.Contains("400"))
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
           
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"Demanda/Demands-Details?id={id}");
                httpResponseMessage.EnsureSuccessStatusCode();
                string json = await httpResponseMessage.Content.ReadAsStringAsync();
                DemandaDetailsViewModel? demandaDetailsViewModel = JsonConvert.DeserializeObject<DemandaDetailsViewModel>(json);
                if(demandaDetailsViewModel == null)
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

        [HttpPost]
        public async Task<IActionResult> ChangeStatusInProgress(DemandaUpdateViewModel viewModel)
        {
            try
            {
                HttpResponseMessage message = await _httpClient.PostAsJsonAsync<DemandaUpdateViewModel>("Demanda/ChangeStatusInProgress", viewModel);
                string content = await message.Content.ReadAsStringAsync();

                if (content.Contains("BadRequest"))
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> ChangeStatusInFinished(DemandaUpdateViewModel viewModel)
        {
            return View();
        }


    }
}
