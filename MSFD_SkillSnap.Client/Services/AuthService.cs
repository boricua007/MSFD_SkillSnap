using System.Net.Http.Headers;
using System.Text.Json;
using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace MSFD_SkillSnap.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task InitializeAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                SetBearerToken(token.Trim('"'));
            }
        }

        public async Task<bool> Login(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", new { Email = email, Password = password });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                var token = result.GetProperty("token").GetString();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return false;
                }

                await _localStorage.SetItemAsync("authToken", token);
                SetBearerToken(token);
                return true;
            }
            return false;
        }

        public async Task<bool> Register(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", new { Email = email, Password = password });
            return response.IsSuccessStatusCode;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _http.DefaultRequestHeaders.Authorization = null;
        }

        private void SetBearerToken(string token)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
