using BLL.Impl;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEBPresentationLayer.Models;

namespace WEBPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDemandaService _DemandaService;

        public HomeController(ILogger<HomeController> logger, IDemandaService DemandaService)
        {
            _logger = logger;
            this._DemandaService = DemandaService;
        }

        public async Task<IActionResult> Index()
        {
            //SUBSTITUIR DEPOIS PELA CHAMADA DA WEB API DO DAVI
            DataResponse<Demanda> DemandasResponse = await _DemandaService.GetLast6();
            return View(DemandasResponse.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}