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
using TaskManagement.Repositories;

namespace TaskManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamProjectsController : ControllerBase
    {

        private readonly IRepository<TeamProject, int> teamRepository;

        public TeamProjectsController(IRepository<TeamProject, int> teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        // GET: api/TeamProjects
        [HttpGet]
        public IActionResult Get()
        {
            var teamProjectsList = teamRepository.GeTAll();

            List<TeamProjectDTO> mappedTeamProjects = new List<TeamProjectDTO>();
            foreach (TeamProject item in teamProjectsList)
            {
                mappedTeamProjects.Add(item.ToDto());
            }

            return Ok(mappedTeamProjects);
        }

        // GET: api/TeamProjects/5
        [HttpGet("{id}", Name = "GetTeamProject")]
        public IActionResult Get(int id)
        {
            TeamProject teamProject = teamRepository.GetById(id);

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
            teamRepository.Insert(teamProject);
            teamRepository.Save();
            return Ok(teamProject.ToDto());
        }

        // PUT: api/TeamProjects/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody] TeamProject teamProject)
        {
            if(id == null || id != teamProject.IdTeamProject)
            {
                if (teamProject.IdTeamProject == null)
                {
                    teamProject.IdTeamProject = id;
                }
                else
                {
                    return NotFound();
                }                
            }

            try
            {
                teamRepository.Update(teamProject);
                teamRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (teamRepository.GetById(teamProject.IdTeamProject.GetValueOrDefault()) == null)
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
