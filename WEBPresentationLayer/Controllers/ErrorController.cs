using Microsoft.AspNetCore.Mvc;

namespace WEBPresentationLayer.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult StatusCode(int id)
        {
            return View();
        }
    }
}
