using Microsoft.AspNetCore.Mvc;
using MSFD_SkillSnap.Api.Data;
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
            var usersToAdd = new List<PortfolioUser>
            {
                new PortfolioUser
                {
                    Name = "Daisy Allen",
                    Bio = ".NET Full-Stack Software Developer",
                    ProfileImageUrl = "https://placehold.co/200x200"
                },
                new PortfolioUser
                {
                    Name = "Alex Rivera",
                    Bio = "Frontend Developer",
                    ProfileImageUrl = "https://placehold.co/200x200"
                },
                new PortfolioUser
                {
                    Name = "Jordan Lee",
                    Bio = "Cloud Application Developer",
                    ProfileImageUrl = "https://placehold.co/200x200"
                },
                new PortfolioUser
                {
                    Name = "Morgan Chen",
                    Bio = "Backend Engineer",
                    ProfileImageUrl = "https://placehold.co/200x200"
                },
                new PortfolioUser
                {
                    Name = "Taylor Brooks",
                    Bio = "Full-Stack Developer",
                    ProfileImageUrl = "https://placehold.co/200x200"
                }
            };

            var usersNeeded = Math.Max(0, 5 - _context.PortfolioUsers.Count());
            _context.PortfolioUsers.AddRange(usersToAdd.Take(usersNeeded));
            _context.SaveChanges();

            var users = _context.PortfolioUsers.OrderBy(user => user.Id).ToList();
            var projects = new[]
            {
                new Project { Title = "Portfolio Site", Description = "Blazor portfolio application", ImageUrl = "https://placehold.co/300x200" },
                new Project { Title = "Task Tracker", Description = "Task management web application", ImageUrl = "https://placehold.co/300x200" },
                new Project { Title = "Weather Dashboard", Description = "Weather data dashboard", ImageUrl = "https://placehold.co/300x200" },
                new Project { Title = "Recipe Planner", Description = "Meal planning application", ImageUrl = "https://placehold.co/300x200" },
                new Project { Title = "Fitness Journal", Description = "Workout tracking application", ImageUrl = "https://placehold.co/300x200" }
            };

            var skills = new[]
            {
                new Skill { Name = "C#", Level = "Intermediate" },
                new Skill { Name = "Blazor", Level = "Intermediate" },
                new Skill { Name = "SQL", Level = "Advanced" },
                new Skill { Name = "RESTful APIs", Level = "Intermediate" },
                new Skill { Name = "Azure", Level = "Beginner" }
            };

            var projectsNeeded = Math.Max(0, 5 - _context.Projects.Count());
            var skillsNeeded = Math.Max(0, 5 - _context.Skills.Count());

            _context.Projects.AddRange(projects.Take(projectsNeeded).Select((project, index) =>
            {
                project.PortfolioUserId = users[index % users.Count].Id;
                return project;
            }));

            _context.Skills.AddRange(skills.Take(skillsNeeded).Select((skill, index) =>
            {
                skill.PortfolioUserId = users[index % users.Count].Id;
                return skill;
            }));

            _context.SaveChanges();

            return Ok(new
            {
                message = "Database seeded successfully.",
                portfolioUsers = _context.PortfolioUsers.Count(),
                projects = _context.Projects.Count(),
                skills = _context.Skills.Count()
            });
        }
    }
}
