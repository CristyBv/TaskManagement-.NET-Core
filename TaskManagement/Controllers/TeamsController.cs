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
using TaskManagement.Repositories;

namespace TaskManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IRepository<Team, int> teamRepository;

        public TeamsController(IRepository<Team, int> teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        // GET: api/Team
        [HttpGet]
        public IActionResult Get()
        {
            var teamsList = teamRepository.GeTAll();

            List<TeamDTO> mappedTeams = new List<TeamDTO>();
            foreach (Team item in teamsList)
            {
                mappedTeams.Add(item.ToDto());
            }

            return Ok(mappedTeams);
        }

        // GET: api/Team/5
        [HttpGet("{id}", Name = "GetTeams")]
        public IActionResult Get(int id)
        {
            Team team = teamRepository.GetById(id);

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
            teamRepository.Insert(team);
            teamRepository.Save();
            return Ok(team.ToDto());
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody] Team team)
        {
            if (id == null || id != team.IdTeam)
            {
                if (team.IdTeam == null)
                {
                    team.IdTeam = id;
                }
                else
                {
                    return BadRequest();
                }                
            }

            try
            {
                teamRepository.Update(team);
                teamRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (teamRepository.GetById(team.IdTeam.GetValueOrDefault()) == null)
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
        public IActionResult Delete(int id)
        {
            if(teamRepository.Delete(id) == -1)
            {
                return NotFound();
            }

            teamRepository.Save();
            return Ok();
        }
    }
}
