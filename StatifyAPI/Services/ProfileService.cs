using StatifyModels;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;

namespace StatifyServices.Services
{
    public class ProfileService
    {
        private readonly string ClientId = "de733e5c3f6b418a97c787f8abe82ba5";
        private readonly string ClientSecret = "3f94febc620f4224a7dbbc7bd0580d4e";
        private readonly string RedirectUri = "https://your-redirect-uri.com";
        private readonly string SpotifyApiUrl = "https://api.spotify.com/v1/";

        public async Task GetUser()
        {
            


        }

        public async Task<User> FetchProfileAsync(string token)
        {
            var user = new User();
            using (var httpClient = new HttpClient())
            {
                // Set the correct Authorization header format
                httpClient.DefaultRequestHeaders.Authorization = null;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync($"{SpotifyApiUrl}");

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    user = JsonSerializer.Deserialize<User>(responseString);
                    return user;
                }

                return null;
            }
        }


    }
}
