using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Models;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace TaskManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private ApplicationDbContext context;
        private readonly IMapper mapper;

        public TasksController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Task
        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasksList = context.Tasks
                .Include(t => t.Creator)
                .ThenInclude(t => t.Team)
                .Include(t => t.Member)
                .ThenInclude(t => t.Team)
                .Include(t => t.Project)
                .ToList();

            List<TaskDTO> mappedTasks = mapper.Map<List<TaskDTO>>(tasksList);
            return Ok(mappedTasks);
        }

        // GET: api/Task/5
        [HttpGet("{id}", Name = "GetTask")]
        public IActionResult GetTask(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }                

            var task = context.Tasks
                .Include(t => t.Creator)
                .Include(t => t.Member)
                .Include(t => t.Project)
                .FirstOrDefault(t => t.IdTask == id);

            if(task == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TaskDTO>(task));
        }

        // POST: api/Task
        [HttpPost]
        public IActionResult Create([FromBody] Task task)
        {
            context.Add(task);
            context.SaveChanges();
            return Ok(mapper.Map<TaskDTO>(task));
        }

        // PUT: api/Task/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody] Task task)
        {
            if(id == null || id != task.IdTask)
            {
                if (task.IdTask == null)
                    return BadRequest();
                return BadRequest();
            }

            try
            {
                context.Update(task);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!context.Tasks.Any(p => p.IdTask == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(mapper.Map<TaskDTO>(task));           
            
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var task = context.Tasks.Find(id);

            if(task == null)
            {
                return NotFound();
            }

            context.Remove(task);
            context.SaveChanges();
            return Ok();
        }
    }
}
