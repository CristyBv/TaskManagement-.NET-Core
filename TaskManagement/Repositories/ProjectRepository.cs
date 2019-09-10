using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class ProjectRepository : RepositoryBase<Project, int>, IRepository<Project, int>
    {

        public ProjectRepository(ApplicationDbContext context) : base(context, typeof(Project))
        {
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
    }
}
