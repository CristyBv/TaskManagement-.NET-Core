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

        internal virtual IEnumerable<Task> SortByTitle(IEnumerable<Task> list, bool desc)
        {
            var tasks = list;
            if (list == null)
            {
                tasks = this.GeTAll();
            }

            if (desc)
            {
                return tasks.OrderByDescending(t => t.Title);
            }
            else
            {
                return tasks.OrderBy(t => t.Title);
            }
        }

        internal virtual IEnumerable<Task> SortByStartDate(IEnumerable<Task> list, bool desc)
        {
            var tasks = list;
            if (list == null)
            {
                tasks = this.GeTAll();
            }

            if (desc)
            {
                return tasks.OrderByDescending(t => t.StartDate);
            }
            else
            {
                return tasks.OrderBy(t => t.StartDate);
            }
        }

        internal virtual IEnumerable<Task> SortByDeadline(IEnumerable<Task> list, bool desc)
        {
            var tasks = list;
            if (list == null)
            {
                tasks = this.GeTAll();
            }

            if (desc)
            {
                return tasks.OrderByDescending(t => t.Deadline);
            }
            else
            {
                return tasks.OrderBy(t => t.Deadline);
            }
        }

        internal virtual IEnumerable<Task> SortByPriority(IEnumerable<Task> list, bool desc)
        {
            var tasks = list;
            if (list == null)
            {
                tasks = this.GeTAll();
            }

            if (desc)
            {
                return tasks.OrderByDescending(t => t.Priority);
            }
            else
            {
                return tasks.OrderBy(t => t.Priority);
            }
        }

        internal virtual IEnumerable<Task> SortByStatus(IEnumerable<Task> list, bool desc)
        {
            var tasks = list;
            if (list == null)
            {
                tasks = this.GeTAll();
            }

            if (desc)
            {
                return tasks.OrderByDescending(t => t.Status);
            }
            else
            {
                return tasks.OrderBy(t => t.Status);
            }
        }

        internal virtual IEnumerable<Task> SortByCreatorUserName(IEnumerable<Task> list, bool desc)
        {
            var tasks = list;
            if (list == null)
            {
                tasks = this.GeTAll();
            }

            if (desc)
            {
                return tasks.OrderByDescending(t => t.Creator.UserName);
            }
            else
            {
                return tasks.OrderBy(t => t.Creator.UserName);
            }
        }

        internal virtual IEnumerable<Task> SortByMemberUserName(IEnumerable<Task> list, bool desc)
        {
            var tasks = list;
            if (list == null)
            {
                tasks = this.GeTAll();
            }

            if (desc)
            {
                return tasks.OrderByDescending(t => t.Member.UserName);
            }
            else
            {
                return tasks.OrderBy(t => t.Member.UserName);
            }
        }

        internal virtual IEnumerable<Task> SortByProjectTitle(IEnumerable<Task> list, bool desc)
        {
            var tasks = list;
            if(list == null)
            {
                tasks = this.GeTAll();
            }
            if (desc)
            {
                return tasks.OrderByDescending(t => t.Project.Title);                
            }
            else
            {
                return tasks.OrderBy(t => t.Project.Title);
            }
        }
    }
}
