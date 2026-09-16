using Microsoft.AspNetCore.Mvc;
using MSFD_SkillSnap.Api.Models;

namespace MSFD_SkillSnap.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly SkillSnapContext _context;

        public ProjectsController(SkillSnapContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            var projects = _context.Projects.ToList();
            return Ok(projects);
        }

        [HttpPost]
        public IActionResult AddProject(ProjectCreateRequest request)
        {
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
    }
}
