using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using WEBPresentationLayer.Models.Demanda;

namespace WEBPresentationLayer.Controllers
{
    [Authorize(Policy = "RequireFuncOrAdm")]
    public class DemandaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;
        public DemandaController(HttpClient httpClient, IDistributedCache cache)
        {
            httpClient.BaseAddress = new Uri("https://taskmanagervalidator.azurewebsites.net/");
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ClaimsPrincipal userLogado = this.User;
                string token = userLogado.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await _httpClient.GetAsync("Demanda/All-Demands");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        List<DemandaSelectViewModel>? chamado = JsonConvert.DeserializeObject<List<DemandaSelectViewModel>>(json);
                        if (chamado == null)
                        {
                            return RedirectToAction("StatusCode", "Error");
                        }
                        // criar uma lista demandaselectviewmodel - item1
                        //fazer um foreach com chamado - item2
                        //dentro do for fazer o if 
                        //caso a data for maior trocar a propriedade do status
                        //adcionar todo item do for na lista do item1
                        //no return view retornar a lista populada do item1
                        return View(chamado);
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
        [HttpPost]
        public async Task<IActionResult> Index(string status)
        {
            try
            {
                ClaimsPrincipal userLogado = this.User;
                string token = userLogado.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await _httpClient.GetAsync("Demanda/All-Demands");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        List<DemandaSelectViewModel>? chamado = JsonConvert.DeserializeObject<List<DemandaSelectViewModel>>(json);
                        if (chamado == null)
                        {
                            return RedirectToAction("StatusCode", "Error");
                        }
                        chamado = chamado.Where(c => c.StatusDaDemanda.ToString() == status).ToList();
                        // criar uma lista demandaselectviewmodel - item1
                        //fazer um foreach com chamado - item2
                        //dentro do for fazer o if 
                        //caso a data for maior trocar a propriedade do status
                        //adcionar todo item do for na lista do item1
                        //no return view retornar a lista populada do item1
                        //sql job  azure function
                        return View(chamado);
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
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage message = await _httpClient.PostAsJsonAsync<DemandaInsertViewModel>("Demanda/Insert-Demands", viewModel);

                    if (!message.IsSuccessStatusCode)
                        return RedirectToAction("StatusCode", "Error");

                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction("StatusCode", "Error");
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
                ClaimsPrincipal claimsPrincipal = this.User;
                string token = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await _httpClient.GetAsync($"Demanda/Edit-Demands?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        DemandaUpdateViewModel? chamado = JsonConvert.DeserializeObject<DemandaUpdateViewModel>(json);
                        if (chamado == null)
                            return RedirectToAction("StatusCode", "Error");

                        return View(chamado);
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
        [HttpPost]
        public async Task<IActionResult> Edit(DemandaUpdateViewModel viewModel)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = this.User;
                string token = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync<DemandaUpdateViewModel>("Demanda/Edit-Demands", viewModel);

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
                ClaimsPrincipal claimsPrincipal = this.User;
                string token = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"Demanda/Demands-Details?id={id}");
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string json = await httpResponseMessage.Content.ReadAsStringAsync();
                        DemandaDetailsViewModel? demandaDetailsViewModel = JsonConvert.DeserializeObject<DemandaDetailsViewModel>(json);
                        if (demandaDetailsViewModel == null)
                        {
                            return RedirectToAction("StatusCode", "Error");
                        }
                        return View(demandaDetailsViewModel);
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

        [HttpPost]
        public async Task<IActionResult> ChangeStatusInProgress(DemandaProgressViewModel viewModel)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = this.User;
                string token = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;

                if (!string.IsNullOrEmpty(token))
                {
                    var request = new DemandaProgressViewModel()
                    {
                        DataFim = viewModel.DataFim,
                        DescricaoCurta = viewModel.DescricaoCurta,
                        DescricaoDetalhada = viewModel.DescricaoDetalhada,
                        DataInicio = viewModel.DataInicio,
                        StatusDaDemanda = Entities.Enums.StatusDemanda.Andamento,
                        Nome = viewModel.Nome,
                        ID = viewModel.ID
                    };
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage message = await _httpClient.PostAsJsonAsync<DemandaProgressViewModel>("Demanda/ChangeStatusInProgress", request);
                    if (message.IsSuccessStatusCode)
                    {
                        string content = await message.Content.ReadAsStringAsync();
                        return RedirectToAction(nameof(Index));
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
        [HttpPost]
        public async Task<IActionResult> ChangeStatusInFinished(DemandaFinishedViewModel viewModel)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = this.User;
                string token = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage message = await _httpClient.PostAsJsonAsync<DemandaFinishedViewModel>("Demanda/ChangeStatusInFinished", viewModel);
                    if (message.IsSuccessStatusCode)
                    {
                        string content = await message.Content.ReadAsStringAsync();
                        return RedirectToAction(nameof(Index));
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
        [HttpPost("VerifyFile")]

        public async Task<IActionResult> ChangeStatusInFinished(IFormFile formFile)
        {
            try
            {
                MultipartFormDataContent content = new();
                StreamContent fileContent = new(formFile.OpenReadStream());
                content.Add(fileContent, formFile.Name, formFile.FileName);

                var jsonPayload = "";
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonPayload);
                StreamContent jsonContent = new(new MemoryStream(jsonBytes));
                jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                content.Add(jsonContent, formFile.Name, formFile.FileName);

                var response = await _httpClient.PostAsync("Demanda/ValidateArchive", content);
                if (response.IsSuccessStatusCode)
                {
                    return View(nameof(Index));
                }
                return RedirectToAction("StatusCode", "Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("StatusCode", "Error");
            }
        }

    }
}

