using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class TeamRepository : RepositoryBase<Team, int>, IRepository<Team, int>
    {

        public TeamRepository(ApplicationDbContext context) : base(context, typeof(Team))
        {
        }

        public override IQueryable<object> GeTAll()
        {
            return ((IQueryable<Team>)base.GeTAll())
                .Include(t => t.Members)
                .Include(t => t.TeamProjects);
        }

        public Team GetById(int Id)
        {
            return context.Teams
                .Include(t => t.Members)
                .Include(t => t.TeamProjects)
                .FirstOrDefault(t => t.IdTeam == Id);
        }
    }
}
