using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Models;
using TaskManagement.Repositories;

namespace TaskManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRepository<ApplicationUser, string> userRepository;

        public UsersController(IMapper mapper, IRepository<ApplicationUser, string> userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            List<UserDTO> mappedUsers = mapper.Map<List<UserDTO>>(userRepository.GeTAll());
            return Ok(mappedUsers);
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(string id)
        {
            var user = userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UserDTO>(user));
        }

        // PUT: api/Users/5
        [HttpPut("team/{id}")]
        public IActionResult ChangeTeam(string id, [FromBody] ApplicationUser user)
        {
            UserRepository userRepositoryChild = userRepository as UserRepository;
            int result = userRepositoryChild.ChangeTeam(id, user);
            if(result == -1)
            {
                return NotFound();
            }
            else if(result == -2)
            {
                return BadRequest();
            }

            try
            {
                userRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;               
            }

            return Ok(mapper.Map<UserDTO>(user));
        }
    }
}
