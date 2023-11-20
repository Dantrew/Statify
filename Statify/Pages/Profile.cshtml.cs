using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatifyModels;
using StatifyServices.Services;

namespace Statify.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ProfileService _profileService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileModel(ProfileService profileService, IHttpContextAccessor httpContextAccessor)
        {
            _profileService = profileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public User User { get; set; }
        public async void OnGet()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[".AspNetCore.Session"];
            var code = HttpContext.Request.Query["code"];
            var state = HttpContext.Request.Query["state"];


            User = await _profileService.FetchProfileAsync(token);
        }
    }
}
