using Microsoft.AspNetCore.Mvc;
using WEBPresentationLayer.Models.Funcionario;

namespace WEBPresentationLayer.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly HttpClient _httpClient;
        public FuncionarioController(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7054/");
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FuncionariosInsertViewModel viewModel)
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FuncionarioUpdateViewModel viewModel)
        {
          return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

    }
}