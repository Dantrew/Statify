using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatifyAPI.Models;
using StatifyAPI.Services;

namespace StatifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public TokenController(TokenService tokenService)
        {

            _tokenService = tokenService;

        }
        [HttpGet]
        public async Task<Token> Get()
        {
            return await _tokenService.GetToken();
        }
    }
}
