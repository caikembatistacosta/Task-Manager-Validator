using AutoMapper;
using BLL.Interfaces;
using Common;
using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApi.Models.Cliente;
using WebApi.Models.Funcionario;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    [Authorize(Policy = "RequireADM")]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clientesvc;
        private readonly IMapper _mapper;
        private readonly IFuncionarioService _funcionario;

        public ClienteController(IClienteService svc, IMapper mapper, IFuncionarioService funcionarioService)
        {
            this._clientesvc = svc;
            this._mapper = mapper;
            _funcionario = funcionarioService;
        }
        [HttpGet("All-Costumers")]
        public async Task<IActionResult> Index()
        {

            DataResponse<Cliente> responseClientes = await _clientesvc.GetAll();

            if (!responseClientes.HasSuccess)
            {
                return BadRequest();
            }

            List<ClienteSelectViewModel> clientes = _mapper.Map<List<ClienteSelectViewModel>>(responseClientes.Data);
            return Ok(clientes);
        }
        [HttpGet("Insert-Costumer")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost("Insert-Costumer")]
        public async Task<IActionResult> Create([FromBody]ClienteInsertViewModel viewModel)
        {
            
            Cliente cliente = _mapper.Map<Cliente>(viewModel);
            Response response = await _clientesvc.Insert(cliente);

            if (response.HasSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
        [HttpGet("Edit-Costumer")]
        public async Task<IActionResult> Edit(int id)
        {
            Funcionario currentUser = (Funcionario)HttpContext.Items["Funcionario"];
            if(id != currentUser.ID && currentUser.NivelDeAcesso != NivelDeAcesso.Adm)
            {
                return Unauthorized(new { message = "Nivel de acesso não permitido" });
            }
            SingleResponse<Cliente> responseCliente = await _clientesvc.GetById(id);
            if (!responseCliente.HasSuccess)
            {
                return BadRequest(responseCliente.Message);
            }
            Cliente cliente = responseCliente.Item;
            ClienteUpdateViewModel updateViewModel = _mapper.Map<ClienteUpdateViewModel>(cliente);
            return Ok(updateViewModel);

        }
        [HttpPut("Update-Costumer")]
        public async Task<IActionResult> Edit([FromBody]ClienteUpdateViewModel viewModel)
        {
            Cliente cliente = _mapper.Map<Cliente>(viewModel);
            Response response = await _clientesvc.Update(cliente);
            if (!response.HasSuccess)
            {
                return BadRequest(response.Message);
            }
            ViewBag.Errors = response.Message;
            return Ok(cliente);
        }
        [HttpGet("Costumer-Details")]
        public async Task<IActionResult> Details(int id)
        {
            SingleResponse<Cliente> single = await _clientesvc.GetById(id);
            if (!single.HasSuccess)
            {
                return BadRequest(single.Message);
            }
            Cliente cliente = single.Item;
            ClienteDetailsViewModel viewModel = _mapper.Map<ClienteDetailsViewModel>(cliente);
            return Ok(viewModel);
        }

       

    }
}
