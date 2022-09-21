using AutoMapper;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models.Demanda;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    [Authorize(Policy = "RequireFuncOrAdm")]
    public class DemandaController : Controller
    {
        private readonly IDemandaService _Demandasvc;
        private readonly IMapper _mapper;
        private readonly ITokenService token;
        public DemandaController(IDemandaService svc, IMapper mapper, ITokenService tokenService)
        {
            this._Demandasvc = svc;
            this._mapper = mapper;
            token = tokenService;
        }
        [HttpGet("All-Demands")]
        public async Task<IActionResult> Index()
        {
            DataResponse<Demanda> responseDemandas = await _Demandasvc.GetAll();

            if (!responseDemandas.HasSuccess)
            {
                ViewBag.Errors = responseDemandas.Message;
                return BadRequest();
            }
            List<DemandaSelectViewModel> Demandas = _mapper.Map<List<DemandaSelectViewModel>>(responseDemandas.Data);
            return Ok(Demandas);
        }
        [HttpGet("Insert-Demands")]
        public IActionResult Create()
        {
            return Ok();
        }
        [HttpPost("Insert-Demands")]
        public async Task<IActionResult> Create([FromBody] DemandaInsertViewModel viewModel)
        {

            Demanda Demanda = _mapper.Map<Demanda>(viewModel);

            Response response = await _Demandasvc.Insert(Demanda);

            if (!response.HasSuccess)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtRoute("Insert-Demands", new {id = viewModel.ID}, viewModel);
        }
        [HttpGet("Edit-Demands")]
        public async Task<IActionResult> Edit(int id)
        {
            SingleResponse<Demanda> responseDemanda = await _Demandasvc.GetById(id);
            if (!responseDemanda.HasSuccess)
            {
                return BadRequest(responseDemanda.Message);
            }
            Demanda Demanda = responseDemanda.Item;
            DemandaUpdateViewModel updateViewModel = _mapper.Map<DemandaUpdateViewModel>(Demanda);
            return Ok(updateViewModel);

        }
        [HttpPut("Edit-Demands")]
        public async Task<IActionResult> Edit(DemandaUpdateViewModel viewModel)
        {
            Demanda Demanda = _mapper.Map<Demanda>(viewModel);
            Response response = await _Demandasvc.Update(Demanda);
            if (!response.HasSuccess)
            {
                ViewBag.Errors = response.Message;
                return BadRequest();
            }
            return Ok(Demanda);
        }
        [HttpGet("Demands-Details")]
        public async Task<IActionResult> Details(int id)
        {
            SingleResponse<Demanda> single = await _Demandasvc.GetById(id);
            if (!single.HasSuccess)
            {
                return BadRequest(single.Message);
            }
            Demanda Demanda = single.Item;
            DemandaDetailsViewModel viewModel = _mapper.Map<DemandaDetailsViewModel>(Demanda);
            return Ok(viewModel);
        }
        [HttpPost("ChangeStatusInProgress")]
        public async Task<IActionResult> ChangeStatusInProgress(DemandaUpdateViewModel viewModel)
        {
            Demanda Demanda = _mapper.Map<Demanda>(viewModel);
            Response response = await _Demandasvc.ChangeStatusInProgress(Demanda);
            if (response.HasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Errors = response.Message;
            return View(Demanda);
        }


    }
}
