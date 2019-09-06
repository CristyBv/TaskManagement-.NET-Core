using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;

namespace TaskManagement.Repositories
{
    public class TaskRepository : IRepository
    {
        private readonly ApplicationDbContext context;

        public TaskRepository()
        {
            
        }

        public void Delete(Task task)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GeTAll()
        {
            throw new NotImplementedException();
        }

        public Task GetById(int IdTask)
        {
            throw new NotImplementedException();
        }

        public void Insert(Task task)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
