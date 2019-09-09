using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class TaskRepository : IRepository<Task, int>
    {
        private readonly ApplicationDbContext context;

        public TaskRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int Delete(int Id)
        {
            Task task = context.Tasks.Find(Id);
            if(task == null)
            {
                return -1;
            }
            context.Tasks.Remove(task);
            return 1;
        }

        public IEnumerable<Task> GeTAll()
        {
            return context.Tasks
                .Include(t => t.Creator)
                .ThenInclude(t => t.Team)
                .Include(t => t.Member)
                .ThenInclude(t => t.Team)
                .Include(t => t.Project)
                .ToList();
        }

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

        public void Insert(Task entity)
        {
            context.Tasks.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Task entity)
        {
             context.Tasks.Update(entity);            
        }
    }
}
