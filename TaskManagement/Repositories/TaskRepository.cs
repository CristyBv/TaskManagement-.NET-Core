using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class TaskRepository : RepositoryBase<Task, int>, IRepository<Task, int>
    {

        public TaskRepository(ApplicationDbContext context) : base(context, typeof(Task))
        {
        }

        /*public IEnumerable<Task> GeTAll()
        {
            return context.Tasks
                .Include(t => t.Creator)
                .ThenInclude(t => t.Team)
                .Include(t => t.Member)
                .ThenInclude(t => t.Team)
                .Include(t => t.Project)
                .ToList();
        }*/

        public Task GetById(int Id)
        {
            return context.Tasks
                .Include(t => t.Creator)
                .ThenInclude(t => t.Team)
                .Include(t => t.Member)
                .ThenInclude(t => t.Team)
                .Include(t => t.Project)
                .FirstOrDefault(t => t.IdTask == Id);
        }
    }
}
