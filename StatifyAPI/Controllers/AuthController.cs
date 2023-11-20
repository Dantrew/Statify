using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatifyServices.Services;

namespace StatifyServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly LoginService _loginService;

        public AuthController(LoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpGet]
        public void Login()
        {
            var url = _loginService.Login();

        }

    }
}