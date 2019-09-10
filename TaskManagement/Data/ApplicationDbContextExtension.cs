using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Data
{
    public static partial class ApplicationDbContextExtension
    {
        public static IQueryable Query(this ApplicationDbContext context, string entityName) =>
            context.Query(context.Model.FindEntityType(entityName).ClrType);

        public static IQueryable<object> Query(this ApplicationDbContext context, Type entityType) =>
            (IQueryable<object>)((IDbSetCache)context).GetOrAddSet(context.GetDependencies().SetSource, entityType);

        public static IQueryable<object> Set(this ApplicationDbContext _context, Type t)
        {
            return (IQueryable<object>)_context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(_context, null);
        }
    }
}
