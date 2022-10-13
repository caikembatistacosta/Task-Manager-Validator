using AutoMapper;
using BusinessLogicalLayer.Impl;
using BusinessLogicalLayer.Interfaces;
using Shared;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using WebApi.Models.Demanda;
using log4net;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    [Authorize(Policy = "RequireFuncOrAdm")]
    public class DemandaController : Controller
    {
        private readonly IDemandaService _Demandasvc;
        private readonly IMapper _mapper;
        private readonly IClassValidatorService _classValidatorService;
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DemandaController(IDemandaService svc, IMapper mapper, IClassValidatorService classValidatorService)
        {
            this._Demandasvc = svc;
            this._mapper = mapper;
            _classValidatorService = classValidatorService;
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
            ClaimsPrincipal userLogado = this.User;
            string? name = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            Demanda Demanda = _mapper.Map<Demanda>(viewModel);
            log.Info($"O usuário {name} está tentando inserir uma demanda");
            Response response = await _Demandasvc.Insert(Demanda);
            if (!response.HasSuccess)
            {
                log.Info("Falha ao inserir uma demanda");
                return BadRequest(response.Message);
            }
            log.Info($"Sucesso ao inserir a demanda por {name}");
            return Ok(response.Message);
        }
        [HttpGet("Edit-Demands")]
        public async Task<IActionResult> Edit(int id)
        {
            ClaimsPrincipal userLogado = this.User;
            string? name = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            log.Info($"Usuário {name} está buscando uma demanda");
            SingleResponse<Demanda> responseDemanda = await _Demandasvc.GetById(id);
            if (!responseDemanda.HasSuccess)
            {
                log.Info("Demanda não foi encontrada");
                return BadRequest(responseDemanda.Message);
            }
            log.Info("Demanda encotrada com sucesso");
            DemandaUpdateViewModel updateViewModel = _mapper.Map<DemandaUpdateViewModel>(responseDemanda.Item);
            return Ok(updateViewModel);

        }
        [HttpPut("Edit-Demands")]
        public async Task<IActionResult> Edit(DemandaUpdateViewModel viewModel)
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            Demanda demanda = _mapper.Map<Demanda>(viewModel);
            log.Info($"Usuário {user} está tentando atualizar essa demanda");
            Response response = await _Demandasvc.Update(demanda);
            if (!response.HasSuccess)
            {
                log.Info("Erro ao atualizar a demanda");
                ViewBag.Errors = response.Message;
                return BadRequest();
            }
            log.Info("Sucesso ao atualizar a demanda");
            return Ok();
        }
        [HttpGet("Demands-Details")]
        public async Task<IActionResult> Details(int id)
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            log.Info($"Usuário {user} está buscando uma demanda pelo id");
            SingleResponse<Demanda> single = await _Demandasvc.GetById(id);
            if (!single.HasSuccess)
            {
                log.Info("Não foi possível completar a operação");
                return BadRequest(single.Message);
            }
            DemandaDetailsViewModel viewModel = _mapper.Map<DemandaDetailsViewModel>(single.Item);
            log.Info("Sucesso ao completar a operação");
            return Ok(viewModel);
        }
        [HttpPost("ChangeStatusInProgress")]
        public async Task<IActionResult> ChangeStatusInProgress(DemandaProgressViewModel viewModel)
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            log.Info($"Usuário {user} está aceitando a demanda");
            Demanda demanda = _mapper.Map<Demanda>(viewModel);
            Response response = await _Demandasvc.ChangeStatusInProgress(demanda);
            if (response.HasSuccess)
            {
                log.Info("Sucesso ao aceitar a demanda");
                return Ok(response.Message);
            }
            log.Info("Erro ao aceitar a demanda");
            return BadRequest(response.Message);
        }
        [HttpPost("ChangeStatusInFinished")]
        public async Task<IActionResult> ChangeStatusInFinished([FromBody] DemandaUpdateViewModel viewModel)
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            log.Info($"O usuário {user} está tentando finalizar a demanda");
            Demanda demanda = _mapper.Map<Demanda>(viewModel);
            Response response = await _Demandasvc.ChangeStatusInFinished(demanda);
            if (response.HasSuccess)
            {
                log.Info("Sucesso em fechar a demanda");
                return Ok(response.Message);
            }
            log.Info("Falha ao tentar fechar a demanda");
            return BadRequest(response.Message);

        }
        [HttpPost("VerifingFiles")]
        public IActionResult ChangeStatusInFinished(IFormFile formFile)
        {
            ClaimsPrincipal userLogado = this.User;
            string user = userLogado.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            log.Info($"O usuário {user} está tentando validar seu arquivo");
            MemoryStream ms = new();
            formFile.CopyTo(ms);
            ms.Position = 0;
            string conteudo = Encoding.UTF8.GetString(ms.ToArray());

            SingleResponse<ReflectionEntity> singleResponse = _classValidatorService.Validator(conteudo);
            if (singleResponse.HasSuccess)
            {
                log.Info("Sucesso na validação do arquivo");
                return Ok(singleResponse.Message);
            }
            log.Info("Falha ao validar o arquivo, tentando novamente");
            while (!singleResponse.HasSuccess)
            {
                SingleResponse<ReflectionEntity> response = _classValidatorService.Validator(singleResponse.Item.NewCodeToCompile);
                if (response.HasSuccess)
                {
                    log.Info("Sucesso ao validar o arquivo, retornando a tela");
                    return Ok();
                }
                singleResponse = _classValidatorService.Validator(response.Item.NewCodeToCompile);
                if (singleResponse.HasSuccess)
                {
                    log.Info("Sucesso ao validar o arquivo, retornando a tela");
                    return Ok();
                }
            }
            log.Info("Falha ao validar o arquivo");
            return BadRequest(singleResponse.Message);
        }





    }
}
