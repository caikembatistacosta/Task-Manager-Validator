using BusinessLogicalLayer.Impl;
using BusinessLogicalLayer.Interfaces;
using Shared;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Models;
using log4net;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class HomeController : Controller
    {
        private readonly IDemandaService _DemandaService;
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public HomeController( IDemandaService DemandaService)
        {
            this._DemandaService = DemandaService;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            DataResponse<Demanda> DemandasResponse = await _DemandaService.GetLast6();
            if (!DemandasResponse.HasSuccess)
            {
                log.Warn("Falha ao pegar as ultimas 6 demandas");
                return BadRequest();
            }
            log.Info("Sucesso ao pegar as últimas 6 demandas");
            return Ok(DemandasResponse.Data);
        }

       
    }
}