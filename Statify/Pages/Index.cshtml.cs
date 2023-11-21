using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatifyServices.Services;

namespace Statify.Pages
{
	public class IndexModel : PageModel
	{
		private readonly TokenService _tokenService;

        public IndexModel(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task OnGetAsync()
		{
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
            };

            var token = await _tokenService.GetToken();
        }
	}
}