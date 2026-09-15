using Microsoft.AspNetCore.Mvc;
using MSFD_SkillSnap.Api.Models;

namespace MSFD_SkillSnap.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly SkillSnapContext _context;
        public SeedController(SkillSnapContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Seed()
        {
            if (_context.PortfolioUsers.Any())
                return Ok("Data already exists.");

            var user = new PortfolioUser
            {
                Name = "Daisy Allen",
                Bio = ".NETFull-Stack Software Developer",
                ProfileImageUrl = "https://placehold.co/200x200",
                Projects = new List<Project>
                {
                    new Project { Title = "Portfolio Site", Description = "Blazor + API project", ImageUrl = "https://placehold.co/300x200" }
                },
                Skills = new List<Skill>
                {
                    new Skill { Name = "C#", Level = "Intermediate" },
                    new Skill { Name = "Blazor", Level = "Intermediate" },
                    new Skill { Name = "SQL", Level = "Advanced" },
                    new Skill { Name = "RESTful APIs", Level = "Beginner" }
                }
            };

            _context.PortfolioUsers.Add(user);
            _context.SaveChanges();

            return Ok("Sample data seeded successfully.");
        }
    }
}
