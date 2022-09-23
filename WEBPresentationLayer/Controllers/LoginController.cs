using Entities.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WEBPresentationLayer.Models.Funcionario;
using WEBPresentationLayer.Models.Token;

namespace WEBPresentationLayer.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        public LoginController(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7054/");
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logar(FuncionarioLoginViewModel funcionarioLogin)
        {
            try
            {
                HttpResponseMessage message = await _httpClient.PostAsJsonAsync<FuncionarioLoginViewModel>("Login/Logar", funcionarioLogin);
                if (message.IsSuccessStatusCode)
                {
                    string content = await message.Content.ReadAsStringAsync();
                    FuncionarioLoginViewModel f = JsonConvert.DeserializeObject<FuncionarioLoginViewModel>(content);
                    if (f == null)
                    {
                        return NotFound();
                    }

                    List<Claim> userClaims = new()
                    {
                        new Claim(ClaimTypes.NameIdentifier, f.ID.ToString()),
                        new Claim(ClaimTypes.Email, f.Email),
                        new Claim(ClaimTypes.Sid, f.Token),
                        new Claim(ClaimTypes.Role, NivelDeAcesso.Adm.ToString()),
                        new Claim(ClaimTypes.Role, NivelDeAcesso.Funcionario.ToString())
                    };
                    ClaimsIdentity minhaIdentity = new(userClaims, "Token");

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.Now.AddHours(8)
                    };

                    await HttpContext.SignInAsync(
           CookieAuthenticationDefaults.AuthenticationScheme,
           new ClaimsPrincipal(minhaIdentity),
           authProperties);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                return NotFound();
            }

        }
        [Authorize]
        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Refresh(TokenViewModel tokenView)
        {
            HttpResponseMessage message = await _httpClient.PostAsJsonAsync<TokenViewModel>("Login/Refresh", tokenView);
            string content = await message.Content.ReadAsStringAsync();
            if (!message.IsSuccessStatusCode)
            {
                return NotFound();
            }
            TokenViewModel json = JsonConvert.DeserializeObject<TokenViewModel>(content);
            if (json == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
