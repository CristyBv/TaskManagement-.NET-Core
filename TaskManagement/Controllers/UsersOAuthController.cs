using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Crypts;
using TaskManagement.Models;
using TaskManagement.Repositories;

namespace TaskManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersOAuthController : ControllerBase
    {
        private readonly IRepository<UserOAuth, int> userOAuthRepository;

        public UsersOAuthController(IRepository<UserOAuth, int> userOAuthRepository)
        {
            this.userOAuthRepository = userOAuthRepository;
        }

        [AllowAnonymous]
        [HttpGet("authenticate")]
        public IActionResult Authenticate(string username, string password)
        {
            UserOAuthRepository userOAuthRepositoryChild = userOAuthRepository as UserOAuthRepository;
            SecurityToken token = userOAuthRepositoryChild.Authenticate(username, password);
            if(token == null)
            {
                return BadRequest();
            }

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserOAuth userOAuth)
        {
            userOAuth.Password = userOAuth.Password.HashPassword();
            userOAuthRepository.Insert(userOAuth);
            userOAuthRepository.Save();
            return Ok(userOAuth);
        }
    }
}