﻿using Microsoft.EntityFrameworkCore;
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

        public override IQueryable<object> GeTAll()
        {
            return ((IQueryable<Task>)base.GeTAll())
                .Include(t => t.Creator)
                .ThenInclude(t => t.Team)
                .Include(t => t.Member)
                .ThenInclude(t => t.Team)
                .Include(t => t.Project);
        }

        public Task GetById(int Id)
        {
            return ((IQueryable<Task>)base.GeTAll())
                .Include(t => t.Creator)
                .ThenInclude(t => t.Team)
                .Include(t => t.Member)
                .ThenInclude(t => t.Team)
                .Include(t => t.Project)
                .FirstOrDefault(t => t.IdTask == Id);
        }
    }
}
