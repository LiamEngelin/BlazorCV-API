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
    public class ProjectController : ControllerBase
    {
        private readonly DataContext _DbContext;

        public ProjectController(DataContext dbContext)
        {
            _DbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetAllProjects()
        {
            var projects = await _DbContext.Projects.ToListAsync();
            if (projects == null)
            {
                return NotFound("Projects not found");
            }
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _DbContext.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound("project not found");
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> Createproject(Project newproject)
        {
            _DbContext.Projects.AddAsync(newproject);
            await _DbContext.SaveChangesAsync();
            return Ok(newproject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> Editproject(int id, Project newproject)
        {
            var oldproject = await _DbContext.Projects.FindAsync(id);
            oldproject.Name = newproject.Name;
            oldproject.Description = newproject.Description;
            await _DbContext.SaveChangesAsync();
            return Ok(newproject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> Deleteproject(int id)
        {
            var project = await _DbContext.Projects.FindAsync(id);
            _DbContext.Projects.Remove(project);
            await _DbContext.SaveChangesAsync();
            return Ok($"Removed project {project.Name}");
        }
    }
}
