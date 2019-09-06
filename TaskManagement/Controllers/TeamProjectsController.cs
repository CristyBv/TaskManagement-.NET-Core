using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class TeamProjectsController : ControllerBase
    {

        private readonly ApplicationDbContext context;

        public TeamProjectsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/TeamProjects
        [HttpGet]
        public IActionResult Get()
        {
            var teamProjectsList = context.TeamProjects
                .Include(t => t.Team)
                .Include(t => t.Project)
                .ToList();

            List<TeamProjectDTO> mappedTeamProjects = new List<TeamProjectDTO>();
            foreach (TeamProject item in teamProjectsList)
            {
                mappedTeamProjects.Add(item.ToDto());
            }

            return Ok(mappedTeamProjects);
        }

        // GET: api/TeamProjects/5
        [HttpGet("{id}", Name = "GetTeamProject")]
        public IActionResult Get(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var teamProject = context.TeamProjects
                .Include(t => t.Team)
                .Include(t => t.Project)
                .FirstOrDefault(t => t.IdTeamProject == id);

            if(teamProject == null)
            {
                return NotFound();
            }

            return Ok(teamProject.ToDto());
        }

        // POST: api/TeamProjects
        [HttpPost]
        public IActionResult Post([FromBody] TeamProject teamProject)
        {
            context.Add(teamProject);
            context.SaveChanges();
            return Ok(teamProject.ToDto());
        }

        // PUT: api/TeamProjects/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody] TeamProject teamProject)
        {
            if(id == null || id != teamProject.IdTeamProject)
            {
                if (teamProject.IdTeamProject == null)
                    return BadRequest();
                return NotFound();
            }

            try
            {
                context.Update(teamProject);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.TeamProjects.Any(p => p.IdTeamProject == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(teamProject.ToDto());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var teamProject = context.TeamProjects.Find(id);

            if(teamProject == null)
            {
                return NotFound();
            }

            context.Remove(teamProject);
            context.SaveChanges();
            return Ok();
        }
    }
}
