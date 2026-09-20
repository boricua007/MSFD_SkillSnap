using Microsoft.AspNetCore.Mvc;
using MSFD_SkillSnap.Api.Data;
using MSFD_SkillSnap.Api.DTOs;
using MSFD_SkillSnap.Api.Models;
using Microsoft.AspNetCore.Authorization;
namespace MSFD_SkillSnap.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly SkillSnapContext _context;

        public SkillsController(SkillSnapContext context)
        {
            _context = context;
        }

        // GET: api/skills
        [HttpGet]
        public IActionResult GetSkills()
        {
            var skills = _context.Skills
                .Select(skill => new SkillDto
                {
                    Id = skill.Id,
                    Name = skill.Name,
                    Level = skill.Level,
                    PortfolioUserId = skill.PortfolioUserId
                })
                .ToList();

            return Ok(skills);
        }

        // POST: api/skills
        [Authorize]
        [HttpPost]
        public IActionResult AddSkill(SkillDto newSkill)
        {
            var skill = new Skill
            {
                Name = newSkill.Name,
                Level = newSkill.Level,
                PortfolioUserId = newSkill.PortfolioUserId
            };

            _context.Skills.Add(skill);
            _context.SaveChanges();

            var response = new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Level = skill.Level,
                PortfolioUserId = skill.PortfolioUserId
            };

            return CreatedAtAction(nameof(GetSkills), new { id = skill.Id }, response);
        }

        // DELETE: api/skills/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteSkill(int id)
        {
            var skill = _context.Skills.Find(id);
            if (skill == null)
                return NotFound();

            _context.Skills.Remove(skill);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
