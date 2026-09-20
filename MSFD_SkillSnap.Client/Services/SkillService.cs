using System.Net.Http.Json;
using MSFD_SkillSnap.Client.Models;

namespace MSFD_SkillSnap.Client.Services
{
    public class SkillService
    {
        private readonly HttpClient _http;

        public SkillService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Skill>> GetSkillsAsync()
        {
            return await _http.GetFromJsonAsync<List<Skill>>("api/skills") ?? new List<Skill>();
        }

        public async Task<Skill?> AddSkillAsync(Skill newSkill)
        {
            var response = await _http.PostAsJsonAsync("api/skills", newSkill);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<Skill>()
                : null;
        }
    }
}
