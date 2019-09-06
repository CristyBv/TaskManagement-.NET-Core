using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? IdTask { get; set; }
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
        public int? IdProject { get; set; }
        public virtual Project Project { get; set; }
    }
}
