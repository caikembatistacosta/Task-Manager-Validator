using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace WEBPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;
        public HomeController(HttpClient http, IDistributedCache distributedCache)
        {
            http.BaseAddress = new Uri("https://taskmanagervalidator.azurewebsites.net/");
            _httpClient = http;
            _cache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("Home/Index");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Demanda>? chamado = new();
                    string cacheIndex = _cache.GetString("chamado");
                    if (!string.IsNullOrWhiteSpace(cacheIndex))
                    {
                        chamado = JsonConvert.DeserializeObject<List<Demanda>>(json);
                        if (chamado == null)
                        {
                            return RedirectToAction("StatusCode", "Error");
                        }
                    }
                    else
                    {
                        chamado = JsonConvert.DeserializeObject<List<Demanda>>(json);
                        _cache.SetString("chamado", json);
                    }
                    return View(chamado);
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