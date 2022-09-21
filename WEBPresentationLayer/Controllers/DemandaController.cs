using AutoMapper;
using BLL.ClassValidator;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WEBPresentationLayer.Models.Demanda;

namespace WEBPresentationLayer.Controllers
{
    public class DemandaController : Controller
    {
        private readonly IDemandaService _Demandasvc;
        private readonly IMapper _mapper;
        public DemandaController(IDemandaService svc, IMapper mapper)
        {
            this._Demandasvc = svc;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {

            DataResponse<Demanda> responseDemandas = await _Demandasvc.GetAll();

            if (!responseDemandas.HasSuccess)
            {
                ViewBag.Errors = responseDemandas.Message;
                return View();
            }
            List<DemandaSelectViewModel> Demandas = _mapper.Map<List<DemandaSelectViewModel>>(responseDemandas.Data);
            return View(Demandas);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DemandaInsertViewModel viewModel)
        {

            Demanda Demanda = _mapper.Map<Demanda>(viewModel);

            Response response = await _Demandasvc.Insert(Demanda);

            if (response.HasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Errors = response.Message;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            SingleResponse<Demanda> responseDemanda = await _Demandasvc.GetById(id);
            if (!responseDemanda.HasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            Demanda Demanda= responseDemanda.Item;
            DemandaUpdateViewModel updateViewModel = _mapper.Map<DemandaUpdateViewModel>(Demanda);
            return View(updateViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(DemandaUpdateViewModel viewModel)
        {
            Demanda Demanda = _mapper.Map<Demanda>(viewModel);
            Response response = await _Demandasvc.Update(Demanda);
            if (response.HasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Errors = response.Message;
            return View(Demanda);
        }
        public async Task<IActionResult> Details(int id)
        {
            SingleResponse<Demanda> single = await _Demandasvc.GetById(id);
            if (!single.HasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            Demanda Demanda = single.Item;
            DemandaDetailsViewModel viewModel = _mapper.Map<DemandaDetailsViewModel>(Demanda);
            ViewBag.Action = viewModel.StatusDaDemanda == Entities.Enums.StatusDemanda.Andamento ? "ChangeStatusInFinished" : "ChangeStatusInProgress";
            return View(viewModel);
        }
        [HttpPost]
        //[Route("ChangeStatusInProgress")]
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
        [HttpPost]
        public async Task<IActionResult> ChangeStatusInFinished(DemandaUpdateViewModel viewModel)
        {
            if (viewModel.FileToValidate == null || viewModel.FileToValidate.Length == 0 || Path.GetExtension(viewModel.FileToValidate.FileName) != ".cs")
            {
                return await Details(viewModel.ID);
            }
            MemoryStream ms = new MemoryStream();
            viewModel.FileToValidate.CopyTo(ms);
            ms.Position = 0;
            string conteudo = Encoding.UTF8.GetString(ms.ToArray());
            ClassValidatorService.Validator(conteudo);


            Demanda Demanda = _mapper.Map<Demanda>(viewModel);
            Response response = await _Demandasvc.ChangeStatusInFinished(Demanda);
            if (response.HasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Errors = response.Message;
            return View(Demanda);
        }
        public async Task<IActionResult> UploadFile()
        {
            ClassValidatorService.Validator("");
            return View();
        }


    }
}
