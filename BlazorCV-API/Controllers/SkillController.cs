using BlazorCV_API.Data;
using BlazorCV_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BlazorCV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly DataContext _DbContext;

        public SkillController(DataContext dbContext)
        {
            _DbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Skill>>> GetAllSkills()
        {
            var skills = await _DbContext.Skills.ToListAsync();
            return Ok(skills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkillById(int id)
        {
            var skill = await _DbContext.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound("Skill not found");
            }
            return Ok(skill);
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> CreateSkill(Skill newSkill)
        {
            _DbContext.Skills.AddAsync(newSkill);
            await _DbContext.SaveChangesAsync();
            return Ok(newSkill);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Skill>> EditSkill(int id, Skill newSkill)
        {
            var oldSkill = await _DbContext.Skills.FindAsync(id);
            oldSkill.Name = newSkill.Name;
            oldSkill.Years = newSkill.Years;
            oldSkill.Level = newSkill.Level;
            _DbContext.SaveChangesAsync();
            return Ok(newSkill);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Skill>> DeleteSkill(int id)
        {
            var skill = await _DbContext.Skills.FindAsync(id);
            _DbContext.Skills.Remove(skill);
            _DbContext.SaveChangesAsync();
            return Ok($"Removed skill {skill.Name}");
        }
    }
}
