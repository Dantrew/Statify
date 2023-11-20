using StatifyAPI.Interfacse;
using StatifyAPI.Models;
using System.Text.Json;
using System.Xml.Linq;

namespace StatifyAPI.Services
{
    public class TokenService : ITokenService
    {
        private static Uri _baseAddress = new Uri("https://accounts.spotify.com/api/");
        private static string _clientId = "de733e5c3f6b418a97c787f8abe82ba5";
        private static string _clientSecret = "3f94febc620f4224a7dbbc7bd0580d4e";

        public async Task<Token> GetToken()
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
                }
                else
                {
                    // Handle non-success status code
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }

            return token;
        }
    }
}
