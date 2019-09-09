using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.DTOs;

namespace TaskManagement.Models
{
    public class TeamProject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? IdTeamProject { get; set; }
        public int IdTeam { get; set; }
        public Team Team { get; set; }
        public int IdProject { get; set; }
        public Project Project { get; set; }
    }
}
