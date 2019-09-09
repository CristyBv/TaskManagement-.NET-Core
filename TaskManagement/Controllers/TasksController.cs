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
using TaskManagement.Repositories;
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
        private readonly IMapper mapper;
        private readonly IRepository<Task, int> taskRepository;

        public TasksController(IMapper mapper, IRepository<Task, int> repository)
        {
            this.mapper = mapper;
            this.taskRepository = repository;
        }

        // GET: api/Task
        [HttpGet]
        public IActionResult GetTasks()
        {         
            List<TaskDTO> mappedTasks = mapper.Map<List<TaskDTO>>(taskRepository.GeTAll());
            return Ok(mappedTasks);
        }

        // GET: api/Task/5
        [HttpGet("{id}", Name = "GetTask")]
        public IActionResult GetTask(int id)
        {
            Task task = taskRepository.GetById(id);

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
            taskRepository.Insert(task);
            taskRepository.Save();
            return Ok(mapper.Map<TaskDTO>(task));
        }

        // PUT: api/Task/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody] Task task)
        {
            if(id == null || id != task.IdTask)
            {
                if (task.IdTask == null)
                {
                    task.IdTask = id;
                }
                else
                {
                    return BadRequest();
                }                
            }

            try
            {
                taskRepository.Update(task);
                taskRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(taskRepository.GetById(task.IdTask.GetValueOrDefault()) == null)
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
        public IActionResult Delete(int id)
        {
            if(taskRepository.Delete(id) == -1)
            {
                return NotFound();
            }

            taskRepository.Save();

            return Ok();
        }

        [HttpPost("filter")]
        public IActionResult Filter([FromBody] FilterDTO filter)
        {
            IEnumerable<Task> tasks = taskRepository.GeTAll();
            int TotalItems = tasks.Count();

            if (filter.Sort != null)
            {
                switch (filter.Sort.ToLower())
                {
                    case "title":
                        tasks = tasks.OrderBy(t => t.Title);
                        break;
                    case "startdate":
                        tasks = tasks.OrderBy(t => t.StartDate);
                        break;
                    case "deadline":
                        tasks = tasks.OrderBy(t => t.Deadline);
                        break;
                    case "priority":
                        tasks = tasks.OrderBy(t => t.Priority);
                        break;
                    case "status":
                        tasks = tasks.OrderBy(t => t.Status);
                        break;
                    case "creator":
                        tasks = tasks.OrderBy(t => t.Creator.UserName);
                        break;
                    case "member":
                        tasks = tasks.OrderBy(t => t.Member.UserName);
                        break;
                    case "project":
                        tasks = tasks.OrderBy(t => t.Project.Title);
                        break;
                }

                if (filter.Desc)
                {
                    tasks = tasks.Reverse();
                }
            }

            if (filter.Page != null)
            {
                if (filter.Limit != null)
                {
                    tasks = tasks.Skip(filter.Limit.GetValueOrDefault() * (filter.Page.GetValueOrDefault() - 1)).Take(filter.Limit.GetValueOrDefault());
                }
                else
                {
                    tasks = tasks.Take(filter.Limit.GetValueOrDefault());
                }
            }

            if(filter.Search != null && filter.Search.Length != 0)
            {
                tasks = tasks.Where(t => t.Title.ToLower().Contains(filter.Search.ToLower()));
            }

            List<TaskDTO> mappedTasks = mapper.Map<List<TaskDTO>>(tasks);

            return Ok(mappedTasks.GroupBy(t => t.Creator.UserName));
        }
    }
}
