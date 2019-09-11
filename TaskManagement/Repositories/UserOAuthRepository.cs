using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Crypts;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class UserOAuthRepository : RepositoryBase<UserOAuth, int>, IRepository<UserOAuth, int>
    {
        private readonly AppSettings appSettings;

        public UserOAuthRepository(ApplicationDbContext context, IOptions<AppSettings> appSettings) : base(context, typeof(UserOAuth))
        {
            this.appSettings = appSettings.Value;
        }

        public UserOAuth GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public virtual SecurityToken Authenticate(string username, string password)
        {

            UserOAuth user = context.UsersOAuth
                .FirstOrDefault(t => t.Username == username && t.Password.VerifyPasswordHash(password) == true);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }
    }
}
