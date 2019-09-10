using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Data
{
    public static class ApplicationDbContextExtension
    {
        public static IQueryable Set(this ApplicationDbContext context, string entityName) =>
            context.Set(context.Model.FindEntityType(entityName).ClrType);

        /*public static IQueryable<object> Query(this ApplicationDbContext context, Type entityType) =>
            (IQueryable<object>)((IDbSetCache)context).GetOrAddSet(context.GetDependencies().SetSource, entityType);*/

        public static IQueryable<object> Set(this ApplicationDbContext context, Type entityType)
        {
            return (IQueryable<object>)context.GetType().GetMethod("Set").MakeGenericMethod(entityType).Invoke(context, null);
        }
    }
}
