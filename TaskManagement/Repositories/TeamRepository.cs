using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class TeamRepository : IRepository<Team, int>
    {
        private readonly ApplicationDbContext context;

        public TeamRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int Delete(int Id)
        {
            Team team = context.Teams.Find(Id);
            if(team == null)
            {
                return -1;
            }
            context.Teams.Remove(team);
            return 1;
        }

        public IEnumerable<Team> GeTAll()
        {
            return context.Teams
                .Include(t => t.Members)
                .Include(t => t.TeamProjects)
                .ToList();
        }

        public Team GetById(int Id)
        {
            return context.Teams
                .Include(t => t.Members)
                .Include(t => t.TeamProjects)
                .FirstOrDefault(t => t.IdTeam == Id);
        }

        public void Insert(Team entity)
        {
            context.Teams.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Team entity)
        {
            context.Teams.Update(entity);
        }
    }
}
