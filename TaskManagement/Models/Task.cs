using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Models
{
    public class Task
    {
        [Key]
        public string IdTask { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }

        public string IdCreator { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public string IdMember { get; set; }
        public virtual ApplicationUser Member { get; set; }
        public string IdProject { get; set; }
        public virtual Project Project { get; set; }
    }

    /*public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> entity)
        {

        }
    }*/
}
