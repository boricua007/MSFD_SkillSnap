using Microsoft.AspNetCore.Mvc;
using MSFD_SkillSnap.Api.Data;
using MSFD_SkillSnap.Api.Models;
using Microsoft.AspNetCore.Authorization;

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


        // GET: api/projects
        [HttpGet]
        public IActionResult GetProjects()
        {
            var projects = _context.Projects.ToList();
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
