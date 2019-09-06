using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? IdTeam { get; set; }
        public virtual Team Team { get; set; }


        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Task> Creations { get; set; }
    }

    /*public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            
        }
    }*/
}
