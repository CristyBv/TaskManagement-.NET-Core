using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;

namespace TaskManagement.Repositories
{
    public class RepositoryBase<T,K>
    {
        protected readonly ApplicationDbContext context;
        private readonly Type objectType;

        public RepositoryBase(ApplicationDbContext context, Type type)
        {
            this.context = context;
            this.objectType = type;
        }

        public virtual void Insert(T entity)
        {
            context.Add(entity);
        }

        public virtual void Save()
        {
            context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            context.Update(entity);
        }

        public virtual int Delete(K Id)
        {
            object entity = context.Find(objectType, Id);
            if (entity == null)
            {
                return -1;
            }
            context.Remove(entity);
            return 1;
        }

        public virtual IQueryable<object> GeTAll()
        {
            return context.Set(objectType);
        }
    }
}
