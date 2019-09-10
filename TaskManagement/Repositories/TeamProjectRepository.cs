using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class TeamProjectRepository : RepositoryBase<TeamProject, int>, IRepository<TeamProject, int>
    {

        public TeamProjectRepository(ApplicationDbContext context) : base(context, typeof(TeamProject))
        {
        }

        public override IQueryable<object> GeTAll()
        {
            return ((IQueryable<TeamProject>)base.GeTAll())
                .Include(t => t.Team)
                .Include(t => t.Project);
        }

        public TeamProject GetById(int Id)
        {
            return ((IQueryable<TeamProject>)base.GeTAll())
                .Include(t => t.Team)
                .Include(t => t.Project)
                .FirstOrDefault(t => t.IdTeamProject == Id);
        }
    }
}
