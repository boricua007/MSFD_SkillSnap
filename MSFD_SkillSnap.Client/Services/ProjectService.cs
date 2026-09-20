using System.Net.Http.Json;
using MSFD_SkillSnap.Client.Models;

namespace MSFD_SkillSnap.Client.Services
{
    public class ProjectService
    {
        private readonly HttpClient _http;

        public ProjectService(HttpClient http)
        {
            _http = http;
        }


        // GET all projects
        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _http.GetFromJsonAsync<List<Project>>("api/projects") ?? new List<Project>();
        }

        // POST new project with error handling
        public async Task<Project?> AddProjectAsync(Project newProject)
        {
            var response = await _http.PostAsJsonAsync("api/projects", newProject);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Project>();
            }

            // Handle errors gracefully
            return null;
        }

    }
}
