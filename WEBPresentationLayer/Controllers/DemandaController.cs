using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using WEBPresentationLayer.Models.Demanda;

namespace WEBPresentationLayer.Controllers
{
    [Authorize(Policy = "RequireAdm")]
    public class DemandaController : Controller
    {
        private readonly HttpClient _httpClient;
        public DemandaController(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7054/");
            _httpClient = httpClient;
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
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
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
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
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
        public async Task<IActionResult> ChangeStatusInProgress(DemandaUpdateViewModel viewModel)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = this.User;
                string token = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                    HttpResponseMessage message = await _httpClient.PostAsJsonAsync<DemandaUpdateViewModel>("Demanda/ChangeStatusInProgress", viewModel);
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
        public async Task<IActionResult> ChangeStatusInFinished(DemandaUpdateViewModel viewModel)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = this.User;
                string token = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    HttpResponseMessage message = await _httpClient.PostAsJsonAsync<DemandaUpdateViewModel>("Demanda/ChangeStatusInProgress", viewModel);
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

                HttpResponseMessage response = await _httpClient.PostAsync("Demanda/VerifingFiles", content);
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
