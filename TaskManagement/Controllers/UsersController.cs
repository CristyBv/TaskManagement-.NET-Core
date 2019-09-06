using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult Get()
        {
            List<ApplicationUser> usersList = context.Users
                .Include(t => t.Team)
                .ToList();

            return Ok(mapper.Map<List<UserDTO>>(usersList));
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = context.Users
                .Include(t => t.Team)
                .FirstOrDefault(t => t.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UserDTO>(user));
        }

        /*// POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] ApplicationUser user)
        {
                
        }*/

        // PUT: api/Users/5
        [HttpPut("team/{id}")]
        public IActionResult ChangeTeam(string id, [FromBody] ApplicationUser user)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var currentUser = context.Users.Find(id);

            if(currentUser == null)
            {
                return NotFound();
            }

            if(user.IdTeam == null)
            {
                return BadRequest();
            }

            currentUser.IdTeam = user.IdTeam;

            try
            {
                context.Update(currentUser);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;               
            }

            return Ok(mapper.Map<UserDTO>(user));
        }

        /* // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
