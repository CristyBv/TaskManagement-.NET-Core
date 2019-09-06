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
    public class TeamsController : ControllerBase
    {
        private ApplicationDbContext context;

        public TeamsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/Team
        [HttpGet]
        public IActionResult Get()
        {
            var teamsList = context.Teams
                .Include(t => t.Members)           
                .Include(t => t.TeamProjects)           
                .ToList();

            List<TeamDTO> mappedTeams = new List<TeamDTO>();
            foreach (Team item in teamsList)
            {
                mappedTeams.Add(item.ToDto());
            }

            return Ok(mappedTeams);
        }

        // GET: api/Team/5
        [HttpGet("{id}", Name = "GetTeams")]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var team = context.Teams
                .Include(t => t.Members)
                .Include(t => t.TeamProjects)
                .FirstOrDefault(t => t.IdTeam == id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team.ToDto());
        }

        // POST: api/Team
        [HttpPost]
        public IActionResult Post([FromBody] Team team)
        {
            context.Add(team);
            context.SaveChanges();
            return Ok(team.ToDto());
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody] Team team)
        {
            if (id == null || id != team.IdTeam)
            {
                if (team.IdTeam == null)
                    return BadRequest();
                return BadRequest();
            }

            try
            {
                context.Update(team);
                context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Teams.Any(p => p.IdTeam == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(team.ToDto());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var team = context.Teams.Find(id);

            if (team == null)
            {
                return NotFound();
            }

            context.Remove(team);
            context.SaveChanges();
            return Ok();
        }
    }
}
