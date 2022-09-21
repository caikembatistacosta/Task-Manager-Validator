using BLL.Impl;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WEBPresentationLayer.Models;
using WEBPresentationLayer.Models.Demanda;

namespace WEBPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient http)
        {
            http.BaseAddress = new Uri("https://localhost:7202/");
            _httpClient = http;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("Home/Index");
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                List<Demanda>? chamado = JsonConvert.DeserializeObject<List<Demanda>>(json);
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

    }
}