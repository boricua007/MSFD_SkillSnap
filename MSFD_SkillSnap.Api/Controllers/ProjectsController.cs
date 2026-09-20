using Microsoft.AspNetCore.Mvc;
using MSFD_SkillSnap.Api.Data;
using MSFD_SkillSnap.Api.DTOs;
using MSFD_SkillSnap.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using MSFD_SkillSnap.Api.Controllers;
using System.Diagnostics;

namespace MSFD_SkillSnap.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly SkillSnapContext _context;
        private readonly IMemoryCache _cache;

        public ProjectsController(SkillSnapContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }


        // GET: api/projects
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            // Caching: Measure the time taken to retrieve projects, either from cache or database
            var stopwatch = Stopwatch.StartNew();

            // Try to get the projects from the cache first before querying the database
            if (!_cache.TryGetValue("projects_cache", out List<ProjectDto> projects))
            {
                Console.WriteLine("Cache miss");

                projects = await _context.Projects.AsNoTracking()
                    .Select(p => new ProjectDto
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        PortfolioUserId = p.PortfolioUserId
                    })
                    .ToListAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(20));

                _cache.Set("projects_cache", projects, cacheOptions);
            }
            else
            {
                Console.WriteLine("Cache hit");
            }

            stopwatch.Stop();
            Console.WriteLine($"Request duration: {stopwatch.ElapsedMilliseconds} ms");

            return Ok(projects);
        }

        // POST: api/projects
        [Authorize]
        [HttpPost]
        public IActionResult AddProject(ProjectCreateRequest request)
        {
            if (!_context.PortfolioUsers.Any(user => user.Id == request.PortfolioUserId))
                return BadRequest($"PortfolioUser with ID {request.PortfolioUserId} does not exist.");

            var newProject = new Project
            {
                Title = request.Title,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                PortfolioUserId = request.PortfolioUserId
            };

            _context.Projects.Add(newProject);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProjects), new { id = newProject.Id }, newProject);
        }

        // DELETE: api/projects/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
