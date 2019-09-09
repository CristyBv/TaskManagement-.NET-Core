using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class UserRepository : IRepository<ApplicationUser, string>
    {

        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int Delete(string Id)
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

        public void Insert(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(ApplicationUser entity)
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

        public virtual IEnumerable<ApplicationUser> SortByEmail(bool desc)
        {
            if(desc)
            {
                return this.GeTAll().OrderByDescending(t => t.Email);
            }
            else
            {
                return this.GeTAll().OrderBy(t => t.Email);
            }            
        }
    }
}
