using Microsoft.AspNetCore.Mvc;

namespace StatifyServices.Services
{
    public class LoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        private readonly string ClientId = "de733e5c3f6b418a97c787f8abe82ba5";
        private readonly string RedirectUri = "https://localhost:7251";
        public string Login()
        {
            var state = GenerateRandomString(16);
            var scope = "user-read-private user-read-email";

            var redirectUrl = $"https://accounts.spotify.com/authorize?" +
                $"response_type=code&" +
                $"client_id={ClientId}&" +
                $"scope={scope}&" +
                $"redirect_uri={RedirectUri}&" +
                $"state={state}";

            _httpContextAccessor.HttpContext.Session.SetString("SpotifyAuthState", state);
            return redirectUrl;
        }
        private string GenerateRandomString(int length)
        {
            // hej hej
            // Implement your logic to generate a random string of the specified length
            // This can be a simple random string generation or a more complex algorithm
            // Ensure that the generated string is URL-safe
            // Example (for simplicity, not recommended for production):
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
