using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class UserRepository : RepositoryBase<ApplicationUser, string>, IRepository<ApplicationUser, string>
    {

        public UserRepository(ApplicationDbContext context) : base(context, typeof(ApplicationUser))
        {
        }

        public override int Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GeTAll()
        {
            return context.Users
                .Include(t => t.Team)
                .ToList();
        }

        public ApplicationUser GetById(string Id)
        {
            return context.Users
                .Include(t => t.Team)
                .FirstOrDefault(t => t.Id == Id);
        }

        public override void Insert(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public virtual int ChangeTeam(string id, ApplicationUser entity)
        {
            ApplicationUser user = context.Users.Find(id);

            if(user == null)
            {
                return -1;
            }
            if(entity.IdTeam == null)
            {
                return -2;
            }

            user.IdTeam = entity.IdTeam;

            context.Update(user);

            return 1;
        }
    }
}
