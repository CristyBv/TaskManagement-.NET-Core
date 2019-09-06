using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Models;

namespace TaskManagement.DTOs
{
    public class TaskDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public virtual UserDTO Creator { get; set; }
        public virtual UserDTO Member { get; set; }
        public virtual ProjectDTO Project { get; set; }
    }
}
