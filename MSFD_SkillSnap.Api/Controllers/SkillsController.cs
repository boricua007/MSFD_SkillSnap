using Microsoft.AspNetCore.Mvc;
using MSFD_SkillSnap.Api.Data;
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
            var skills = _context.Skills.ToList();
            return Ok(skills);
        }

        // POST: api/skills
        [Authorize]
        [HttpPost]
        public IActionResult AddSkill(Skill newSkill)
        {
            _context.Skills.Add(newSkill);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSkills), new { id = newSkill.Id }, newSkill);
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
