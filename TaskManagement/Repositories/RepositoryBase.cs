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

        public void Insert(T entity)
        {
            context.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Update(entity);
        }

        public int Delete(K Id)
        {
            object entity = context.Find(objectType, Id);
            if (entity == null)
            {
                return -1;
            }
            context.Remove(entity);
            return 1;
        }
    }
}
