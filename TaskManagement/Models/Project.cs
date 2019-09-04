using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Models
{
    public class Project
    {
        [Key]
        public string IdProject { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public string IdTeam { get; set; }
        public virtual Team Team { get; set; }


    }
}
