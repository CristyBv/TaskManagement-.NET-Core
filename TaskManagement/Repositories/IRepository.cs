using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public interface IRepository<T,K>
    {
        IEnumerable<T> GeTAll();
        T GetById(K Id);
        void Insert(T entity);
        void Update(T entity);
        int Delete(K Id);
        void Save();
    }
}
