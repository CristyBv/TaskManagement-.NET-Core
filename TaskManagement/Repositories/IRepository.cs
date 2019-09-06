using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Repositories
{
    public interface IRepository
    {
        IEnumerable<Task> GeTAll();
        Task GetById(int IdTask);
        void Insert(Task task);
        void Delete(Task task);
        void Save();
    }
}
