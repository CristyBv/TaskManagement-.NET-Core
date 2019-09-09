using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class TeamProjectRepository : IRepository<TeamProject, int>
    {
        private readonly ApplicationDbContext context;

        public TeamProjectRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int Delete(int Id)
        {
            TeamProject teamProject = context.TeamProjects.Find(Id);
            if (teamProject == null)
            {
                return -1;
            }
            context.TeamProjects.Remove(teamProject);
            return 1;
        }

        public IEnumerable<TeamProject> GeTAll()
        {
            return context.TeamProjects
                .Include(t => t.Team)
                .Include(t => t.Project)
                .ToList();
        }

        public TeamProject GetById(int Id)
        {
            return context.TeamProjects
                .Include(t => t.Team)
                .Include(t => t.Project)
                .FirstOrDefault(t => t.IdTeamProject == Id);
        }

        public void Insert(TeamProject entity)
        {
            context.TeamProjects.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(TeamProject entity)
        {
            context.TeamProjects.Update(entity);
        }
    }
}
