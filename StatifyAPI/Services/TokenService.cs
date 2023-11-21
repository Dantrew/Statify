using StatifyModels;
using System.Text.Json;
using System.Xml.Linq;
using StatifyServices.Interfaces;
namespace StatifyServices.Services
{
    public class TokenService : ITokenService
    {
        private static Uri _baseAddress = new Uri("https://accounts.spotify.com/api/");
        private static string _clientId = "de733e5c3f6b418a97c787f8abe82ba5";
        private static string _clientSecret = "3f94febc620f4224a7dbbc7bd0580d4e";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor; // en kommentar
        }

        public async Task<Token> GetToken()
        {
            var cachedToken = GetCachedTokenFromCookie();

            if (cachedToken == null || IsTokenExpired(cachedToken))
            {
                var newToken = await RequestNewToken();
                SetTokenInCookie(newToken);
                return newToken;
            }
            return cachedToken;
        }

        private Token GetCachedTokenFromCookie()
        {
            var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies["AccessToken"];
            var cookieType = _httpContextAccessor.HttpContext.Request.Cookies["TokenType"];
            var cookieTime = _httpContextAccessor.HttpContext.Request.Cookies["ExpiresIn"] is null ? 0 : int.Parse(_httpContextAccessor.HttpContext.Request.Cookies["ExpiresIn"]);
            var cookieIssueTime = _httpContextAccessor.HttpContext.Request.Cookies["IssueTime"] is null ? DateTime.Now : DateTime.Parse(_httpContextAccessor.HttpContext.Request.Cookies["IssueTime"]);


            return !string.IsNullOrEmpty(cookieValue) ? new Token { AccessToken = cookieValue, TokenType = cookieType, ExpiresIn = cookieTime, IssueTime = cookieIssueTime } : null;
        }

        private void SetTokenInCookie(Token token)
        {
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("AccessToken", token.AccessToken, cookieOptions);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("TokenType", token.TokenType, cookieOptions);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("ExpiresIn", token.ExpiresIn.ToString(), cookieOptions);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("IssueTime", token.IssueTime.ToString(), cookieOptions);
        }

        private bool IsTokenExpired(Token token)
        {
            if (token == null || token.ExpiresIn <= 0 || token.IssueTime == default(DateTime))
            {
                return true; // If the token is null, ExpiresIn is not a positive value, or IssueTime is not set, consider it expired
            }

            var expirationTime = token.IssueTime.AddSeconds(token.ExpiresIn);

            // Return true if the current time is greater than or equal to the expiration time
            return DateTime.Now >= expirationTime;
        }


        private async Task<Token> RequestNewToken()
        {
            var token = new Token();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;

                var requestContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", _clientId },
                { "client_secret", _clientSecret }
            });

                HttpResponseMessage response = await client.PostAsync("token", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    token = JsonSerializer.Deserialize<Token>(responseString);
                    if (token is not null)
                    token.IssueTime = DateTime.Now;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }

            return token ?? throw new NullReferenceException("Token was null");
        }
    }
}