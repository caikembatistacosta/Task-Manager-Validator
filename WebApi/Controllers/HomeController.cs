using BLL.Impl;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly IDemandaService _DemandaService;

        public HomeController( IDemandaService DemandaService)
        {
            this._DemandaService = DemandaService;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            //SUBSTITUIR DEPOIS PELA CHAMADA DA WEB API DO DAVI
            DataResponse<Demanda> DemandasResponse = await _DemandaService.GetAll();
            return Ok(DemandasResponse.Data);
        }

       
    }
}