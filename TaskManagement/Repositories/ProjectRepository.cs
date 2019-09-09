using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class ProjectRepository : IRepository<Project, int>
    {

        private readonly ApplicationDbContext context;

        public ProjectRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int Delete(int Id)
        {
            Project project = context.Projects.Find(Id);
            if (project == null)
            {
                return -1;
            }
            context.Projects.Remove(project);
            return 1;
        }

        public IEnumerable<Project> GeTAll()
        {
            return context.Projects
                .Include(t => t.Tasks)
                .Include(t => t.TeamProjects)
                .ToList();
        }

        public Project GetById(int Id)
        {
            return context.Projects
                .Include(t => t.Tasks)
                .Include(t => t.TeamProjects)
                .FirstOrDefault(t => t.IdProject == Id);
        }

        public void Insert(Project entity)
        {
            context.Projects.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Project entity)
        {
            context.Projects.Update(entity);
        }
    }
}
