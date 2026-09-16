using Microsoft.AspNetCore.Mvc;
using MSFD_SkillSnap.Api.Models;

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

        [HttpGet]
        public IActionResult GetSkills()
        {
            var skills = _context.Skills.ToList();
            return Ok(skills);
        }

        [HttpPost]
        public IActionResult AddSkill(Skill newSkill)
        {
            _context.Skills.Add(newSkill);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSkills), new { id = newSkill.Id }, newSkill);
        }
    }
}
