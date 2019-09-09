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
    public class ProjectsController : ControllerBase
    {

         private readonly IRepository<Project, int> projectRepository;

        public ProjectsController(IRepository<Project, int> projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        // GET: api/Project
        [HttpGet]
        public IActionResult Get()
        {
            var projectsList = projectRepository.GeTAll();

            List<ProjectDTO> mappedProjects = new List<ProjectDTO>();
            foreach (Project item in projectsList)
            {
                mappedProjects.Add(item.ToDto());
            }

            return Ok(mappedProjects);
        }

        // GET: api/Project/5
        [HttpGet("{id}", Name = "GetProject")]
        public IActionResult Get(int id)
        {
            Project project = projectRepository.GetById(id);

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
            projectRepository.Insert(project);
            projectRepository.Save();
            return Ok(project.ToDto());
        }

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody] Project project)
        {
            if (id == null || id != project.IdProject)
            {
                if (project.IdProject == null)
                {
                    project.IdProject = id;
                }
                else
                {
                    return BadRequest();
                }                
            }

            try
            {
                projectRepository.Update(project);
                projectRepository.Save();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (projectRepository.GetById(project.IdProject.GetValueOrDefault()) == null)
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
        public IActionResult Delete(int id)
        {
            if(projectRepository.Delete(id) == -1)
            {
                return NotFound();
            }

            projectRepository.Save();
            return Ok();
        }
    }
}
