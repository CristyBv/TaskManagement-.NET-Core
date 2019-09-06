using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Models
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? IdTeam { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<TeamProject> TeamProjects { get; set; }

    }
}
