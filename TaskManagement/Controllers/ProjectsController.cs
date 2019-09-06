using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Mappers;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private ApplicationDbContext context;

        public ProjectsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/Project
        [HttpGet]
        public IActionResult Get()
        {
            var projectsList = context.Projects
                .Include(t => t.Tasks)
                .Include(t => t.TeamProjects)
                .ToList();

            List<ProjectDTO> mappedProjects = new List<ProjectDTO>();
            foreach (Project item in projectsList)
            {
                mappedProjects.Add(item.ToDto());
            }

            return Ok(mappedProjects);
        }

        // GET: api/Project/5
        [HttpGet("{id}", Name = "GetProject")]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var project = context.Projects
                .Include(t => t.Tasks)
                .Include(t => t.TeamProjects)
                .FirstOrDefault(t => t.IdProject == id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project.ToDto());
        }

        // POST: api/Project
        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            context.Add(project);
            context.SaveChanges();
            return Ok(project.ToDto());
        }

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody] Project project)
        {
            if (id == null || id != project.IdProject)
            {
                if (project.IdProject == null)
                    return BadRequest();
                return BadRequest();
            }

            try
            {
                context.Update(project);
                context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Projects.Any(p => p.IdProject == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(project.ToDto());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var project = context.Projects.Find(id);

            if (project == null)
            {
                return NotFound();
            }

            context.Remove(project);
            context.SaveChanges();
            return Ok();
        }
    }
}
