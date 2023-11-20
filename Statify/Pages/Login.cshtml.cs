using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatifyModels;
using StatifyServices.Services;

namespace Statify.Pages
{
    public class LoginModel : PageModel
    {
        private readonly LoginService _loginService;
        private readonly ProfileService _profileService;

        public User User { get; set; }

        public LoginModel(LoginService loginService, ProfileService profileService)
        {
            _loginService = loginService;
            _profileService = profileService;
        }

        public async Task OnGetAsync(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                User = await _profileService.FetchProfileAsync(code);
            }
        }

        public IActionResult OnPost()
        {
            var url = _loginService.Login();
            return Redirect(url);
        }
        public IActionResult OnGetSpotifyRedirect(string code, string state)
        {
            // Validate state to prevent CSRF attacks (optional but recommended)
            var storedState = HttpContext.Session.GetString("SpotifyAuthState");
            if (state != storedState)
            {
                // Handle error
                return RedirectToAction("Error");
            }

            // Redirect to the OnGetAsync method with the retrieved code
            return RedirectToAction("OnGetAsync", new { code });
        }
    }

}
